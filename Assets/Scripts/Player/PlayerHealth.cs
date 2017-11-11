using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 
/// This class is purely to control player's health and Die conditions
/// 
/// Die() - Players will have multiple lives, so Destroy cannot be used because all the information that they store will be lost.
///         Instead of destroying the object, set deactivate all of its components and reactive when they respawn.
/// </summary>

public class PlayerHealth : NetworkBehaviour
{
    [Header("Player Options")]
    public float m_maxHealth = 1000f;

    [Header("Player's UI elements")]
    public RectTransform m_healthBar;
    public GameObject m_healthBar_Front;
    public GameObject m_healthBar_Background;

    [Header("Player Debug Options")]
    public bool m_isDead = false;

    [SyncVar(hook ="UpdateHealthBar")]
    public float m_currentHealth;

    [Header("Health Bar Colours")]
    public Color teamColor;
    public Color teamColor_background;
    [Space]
    public Color enemyColor;
    public Color enemyColor_background;

    //false = enemy team
    private bool teamSide;

    [Header("PopUpText")]
    public FloatingText floatingText;
    public Canvas floatingTextCanvas;

    private void Start()
    {
        m_currentHealth = m_maxHealth;
    }

    void UpdateHealthBar(float val)
    {
        if (m_healthBar != null)
            m_healthBar.sizeDelta = new Vector2(val / m_maxHealth * 150f, m_healthBar.sizeDelta.y);
    }


    public void Damage(float dmg)
    {
        //if (!isServer)
        //    return;

        Debug.Log(dmg);
        m_currentHealth -= dmg;
        initDamageText(dmg.ToString(), transform);

        if (m_currentHealth <= 0 && !m_isDead)
        {
            RpcDie();
        }
    }

    public void initDamageText(string text, Transform position)
    {
        Debug.Log(text + " HEY ");
        FloatingText instance = Instantiate(floatingText);
        instance.transform.SetParent(floatingTextCanvas.transform, false);
        instance.setText(text);
    }

    // TODO: Instead of destroying, disable all of it's relative components such as; mesh renderer, collider etc. etc.
    [ClientRpc]
    void RpcDie()
    {
        m_isDead = true;
        print("Die Executed");
        SetActiveState(false);
        //Destroy(gameObject);
    }

    public void Reset()
    {
        m_currentHealth = m_maxHealth;
        SetActiveState(true);
        m_isDead = false;
    }


    void SetActiveState(bool state)
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = state;
        }

        foreach (Canvas c in GetComponentsInChildren<Canvas>())
        {
            c.enabled = state;
        }

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = state;
        }
    }

    public void setTeam(bool team)
    {
        teamSide = team;

        if (teamSide)
        {
            // Team HealthBar
            m_healthBar_Front.GetComponent<Image>().color = teamColor;
            m_healthBar_Background.GetComponent<Image>().color = teamColor_background;
        }
        else
        {
            // Enemy HealthBar
            m_healthBar_Front.GetComponent<Image>().color = enemyColor;
            m_healthBar_Background.GetComponent<Image>().color = enemyColor_background;
        }
    }

}
