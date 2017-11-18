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
    public float respawnTime = 5;

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
    public string DEBUGID;

    [Header("PopUpText")]
    public FloatingText floatingText;
    public Canvas floatingTextCanvas;

    public bool isDead;

    private void Start()
    {
        m_currentHealth = m_maxHealth;

        DEBUGID = transform.name;
    }

    void UpdateHealthBar(float val)
    {
        if (m_healthBar != null)
            m_healthBar.sizeDelta = new Vector2(val / m_maxHealth * 150f, m_healthBar.sizeDelta.y);
    }

    [ClientRpc]
    public void RpcDamage(float dmg, string sourceID)
    {
        if (m_isDead)
            return;

        initDamageText(dmg, false);

        if (!isServer)
            return;

        m_currentHealth -= dmg;

        if (m_currentHealth <= 0 && !m_isDead)
        {
            RpcDie(sourceID);
        }
    }

    [ClientRpc]
    public void RpcHeal(float heal, string sourceID)
    {
        if (m_isDead)
            return;

        // Calculate how much health is missing
        float diff = m_maxHealth - m_currentHealth;

        // Check if difference is less than m_healAmount
        if (diff < heal)
        {
            heal = diff;
        }

        initDamageText(heal, true);

        if (!isServer)
            return;

        m_currentHealth += heal;
    }

    public void initDamageText(float text, bool heal)
    {
        Debug.Log("log1");
        FloatingText instance = Instantiate(floatingText);
        instance.transform.SetParent(floatingTextCanvas.transform, false);
        instance.setText(text, heal);
    }

    // TODO: Instead of destroying, disable all of it's relative components such as; mesh renderer, collider etc. etc.
    [ClientRpc]
    void RpcDie(string sourceID)
    {
        //KILL/Death
        Player sourcePlayer = GameManager.GetPlayer(sourceID);
        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
            Player player = gameObject.GetComponent<Player>();
            player.deaths++;

            PlayerSetup name = gameObject.GetComponent<PlayerSetup>();
            PlayerSetup sourceName = sourcePlayer.GetComponent<PlayerSetup>();
            GameManager.Instance.onPlayerKilledCallback.Invoke(name.m_playerName, sourceName.m_playerName);
        }

        m_isDead = true;
        SetActiveState(false);

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);

        Reset();
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
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
        if (team)
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
