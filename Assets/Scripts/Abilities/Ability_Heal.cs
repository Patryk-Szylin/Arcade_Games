using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ability_Heal : Ability
{
    public float m_healAmount;
    public float m_scaleFactor = 1f;    //Currently as 1, needs to be adjusted when we 

    //// Ability

    private void Start()
    {
        foreach(Transform child in this.transform)
        {
            if(child.gameObject.tag == "HealEffect")
            {
                m_castEffect = child.gameObject.GetComponent<ParticleSystem>();
            }
        }
    }


    [Command]
    public void CmdShootAbility()
    {
        // "GetPlayerOnHit" is a function that belongs to base class "Ability"
        PlayerController pc = GetPlayerOnHit(m_range);

        var effect = pc.GetComponentInChildren<ParticleSystem>();
        var playerHealth = pc.GetComponent<PlayerHealth>();
        effect.Play();


        //CmdPlayEffect();

        //StartCoroutine(PlayEffect(effect));

        if (playerHealth.m_currentHealth < playerHealth.m_maxHealth)
            playerHealth.Damage(-m_healAmount);
    }

    public override void OnAbilityCast()
    {
        base.OnAbilityCast();

        // "GetPlayerOnHit" is a function that belongs to base class "Ability"
        PlayerController pc = GetPlayerOnHit(m_range);

        var effect = pc.GetComponentInChildren<ParticleSystem>();
        var playerHealth = pc.GetComponent<PlayerHealth>();
        effect.Play();


        //CmdPlayEffect();

        //StartCoroutine(PlayEffect(effect));

        if (playerHealth.m_currentHealth < playerHealth.m_maxHealth)
            playerHealth.Damage(-m_healAmount);
    }




    [ClientRpc]
    void RpcEffect()
    {
        if (isLocalPlayer)
            return;
        m_castEffect.Play();
    }

    void PlayEffect()
    {
        if (isLocalPlayer)
            CmdEffects();

        m_castEffect.Play();

    }

    [Command]
    void CmdEffects()
    {
        RpcEffect();
    }


    //[Command]
    //void CmdPlayEffect()
    //{
    //    PlayerController pc = GetPlayerOnHit(m_range);
    //    m_castEffect.Play();
    //    //var effect = pc.GetComponentInChildren<ParticleSystem>();

    //    //effect.Play();
    //}
}
