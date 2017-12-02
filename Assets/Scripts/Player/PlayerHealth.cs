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
    public float m_maxHealth = 10f;

    [Header("Player's UI elements")]
    public RectTransform m_healthBar;

    [Header("Player Debug Options")]
    [SyncVar]
    public bool m_isDead = false;

    [SyncVar(hook = "UpdateHealthBar")] public float m_currentHealth;
    public Player m_lastAttacker;

    public bool m_isDotTagged = false;



    private void Start()
    {
        Reset();
    }

    void UpdateHealthBar(float val)
    {
        if (m_healthBar != null)
            m_healthBar.sizeDelta = new Vector2(val / m_maxHealth * 150f, m_healthBar.sizeDelta.y);
    }

    public void Damage(float dmg, Player attacker = null)
    {
        if (!isServer)
            return;

        if (attacker != null)
            m_lastAttacker = attacker;

        m_currentHealth -= dmg;

        // If last attacker exists, and last attacker is not me then add score
        if (m_lastAttacker != null && m_lastAttacker != this.GetComponent<Player>())
        {
            m_lastAttacker.m_score += (int)dmg;
            
            UI_Scoreboard.Instance.UpdateScoreboard();            
        }

        if (m_currentHealth <= 0 && !m_isDead)
        {
            if(m_lastAttacker != this.GetComponent<Player>())
            {
                m_lastAttacker.m_kills += 1;
            }

            this.GetComponent<Player>().m_deaths += 1;
            UI_Scoreboard.Instance.UpdateScoreboard();

            m_isDead = true;
            RpcDie();            
        }

        m_lastAttacker = null;
    }

    // TODO: Instead of destroying, disable all of it's relative components such as; mesh renderer, collider etc. etc.
    [ClientRpc]
    void RpcDie()
    {
        print("Die Executed");
        SetActiveState(false);
        gameObject.SendMessage("Disable");
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

        this.GetComponent<Rigidbody>().useGravity = state;
        if (state == false)
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }





    public IEnumerator ApplyDoT(float ticks, float dmg, Player owner)
    {

        for (int i = 0; i < ticks; i++)
        {
            print("APPLYING DOT");

            if (m_isDead)
                break;

            Damage(dmg, owner);
            yield return new WaitForSeconds(1f);
        }
    }





}
