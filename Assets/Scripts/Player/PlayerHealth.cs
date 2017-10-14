using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// 
/// This class if purely to control player's health and Die conditions
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
    public bool m_isDead = false;


    [SyncVar(hook ="UpdateHealthBar")]
    private float m_currentHealth;


    // This currently doesnt work, when uncommented. The player dies as soon as it spawns, but why?
    //public float Health {
    //    private get
    //    {
    //        return m_currentHealth;
    //    }
    //    set
    //    {
    //        m_currentHealth -= value;

    //        if (m_currentHealth <= 0 && !m_isDead)
    //            Die();
    //    }}


    private void Start()
    {
        //m_currentHealth = m_maxHealth;
        m_currentHealth = m_maxHealth;

    }

    void UpdateHealthBar(float val)
    {
        if (m_healthBar != null)
            m_healthBar.sizeDelta = new Vector2(val / m_maxHealth * 150f, m_healthBar.sizeDelta.y);
    }


    public void Damage(float dmg)
    {
        if (!isServer)
            return;

        m_currentHealth -= dmg;        

        if (m_currentHealth <= 0 && !m_isDead)
        {
            RpcDie();
        }
            
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


}
