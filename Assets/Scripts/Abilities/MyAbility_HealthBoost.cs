using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "HealthBoost", menuName = "Abilities/Health Boost ", order = 1)]
public class MyAbility_HealthBoost : MyAbility
{
    [Header("Health Specific")]
    public float m_amount;
    
    public override void ApplyTo(PlayerController pc)
    {
        if (!pc)
        {
            Debug.Log("There is no target available to cast " + this.name);
            return;
        }
            
        pc.GetComponent<PlayerHealth>().m_currentHealth += m_amount;
        Instantiate(m_particleEffect, pc.transform.position, Quaternion.identity);
    }

    public override void ApplyToPlayer()
    {
        m_player.GetComponent<PlayerHealth>().m_currentHealth += m_amount;        
    }
}