﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{
    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;
    public List<Ability> m_abilities = new List<Ability>();

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;                     // needs to be moved to an ability
    public bool m_isReloading = false;


    //=========
    private float m_nextReadyTime;
    //private float m_cooldownLeft;
    //bool m_abilityReady = false;

    public List<bool> m_abilitiesReady = new List<bool>();
    public List<float> m_nextAbilityReadyTime = new List<float>();
    public List<float> m_cooldownLeft = new List<float>();


    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (m_abilities[i] != null)
            {
                m_abilitiesReady.Add(true);
                m_nextAbilityReadyTime.Add(0);
                m_cooldownLeft.Add(m_abilities[i].m_cooldown);
            }
            else
                m_cooldownLeft.Add(0);
        }
    }

    public void Cast()
    {
        //CmdSpawnProjectile();

        StartCoroutine("Reload");
    }

    [Command]
    public void Cmd_Cast_01()
    {        
        CastAbility(0);
    }

    [Command]
    public void Cmd_Cast_02()
    {
        CastAbility(1);
    }

    [Command]
    public void Cmd_Cast_03()
    {
        CastAbility(2);
    }

    // Special Ability
    [Command]
    public void Cmd_Cast_04()
    {
        CastAbility(3);
    }

    private void Update()
    {
        UpdateCooldownUI(0);
        UpdateCooldownUI(1);
        UpdateCooldownUI(2);
        //UpdateCooldownUI(3);
    }


    void CastAbility(int index)
    {

        if (m_abilitiesReady[index])
        {
            m_nextAbilityReadyTime[index] = m_abilities[index].m_cooldown + Time.time;
            m_cooldownLeft[index] = m_abilities[index].m_cooldown;
            m_abilities[index].Initilise(m_abilities[index].m_projectilePrefab, m_projectileSpawn);
            m_abilities[index].TriggerAbility();

            // Re-enable ui
            AbilityReady(index, true);
        }
    }

    void UpdateCooldownUI(int index)
    {

        bool cooldownComplete = (Time.time > m_nextAbilityReadyTime[index]);

        if (cooldownComplete)
        {
            AbilityReady(index, false);
            m_abilitiesReady[index] = true;
        }            
        else
        {
            m_abilitiesReady[index] = false;
            CoolDown(index, m_abilities[index].m_cooldown);
        }
    }

    void CoolDown(int index, float cooldown)
    {
        m_cooldownLeft[index] -= Time.deltaTime;
        float roundedCD = Mathf.Round(m_cooldownLeft[index]);
        if (isLocalPlayer)
        {
            UIManager.Instance.m_cooldownDisplayTexts[index].text = roundedCD.ToString();
            UIManager.Instance.m_darkMasks[index].fillAmount = (m_cooldownLeft[index] / cooldown);
        }


    }

    void AbilityReady(int index, bool ui_enabled)
    {
        if (isLocalPlayer)
        {
            UIManager.Instance.m_cooldownDisplayTexts[index].enabled = ui_enabled;
            UIManager.Instance.m_darkMasks[index].enabled = ui_enabled;
        }

    }

}
