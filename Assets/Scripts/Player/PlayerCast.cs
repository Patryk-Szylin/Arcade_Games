using System.Collections;
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
        for (int i = 0; i < m_abilities.Count; i++)
        {
            if(m_abilities[i] != null)
            {
                m_abilitiesReady.Add(true);
                m_nextAbilityReadyTime.Add(0);
                m_cooldownLeft.Add(m_abilities[i].m_cooldown);
            }
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
        if (m_abilitiesReady[0])
        {
            m_nextAbilityReadyTime[0] = m_abilities[0].m_cooldown + Time.time;
            m_cooldownLeft[0] = m_abilities[0].m_cooldown;
            m_abilities[0].Initilise(m_abilities[0].m_projectilePrefab, m_projectileSpawn);
            m_abilities[0].TriggerAbility();
        }
    }

    [Command]
    public void Cmd_Cast_02()
    {
        if (m_abilitiesReady[1])
        {
            m_nextAbilityReadyTime[1] = m_abilities[1].m_cooldown + Time.time;
            m_cooldownLeft[1] = m_abilities[1].m_cooldown;
            m_abilities[1].Initilise(m_abilities[1].m_projectilePrefab, m_projectileSpawn);
            m_abilities[1].TriggerAbility();
        }
    }

    [Command]
    public void Cmd_Cast_03()
    {

        if (m_abilitiesReady[2])
        {
            m_nextAbilityReadyTime[2] = m_abilities[2].m_cooldown + Time.time;
            m_cooldownLeft[2] = m_abilities[2].m_cooldown;
            m_abilities[2].Initilise(m_abilities[2].m_projectilePrefab, m_projectileSpawn);
            m_abilities[2].TriggerAbility();
        }
    }


    // Special Ability
    [Command]
    public void Cmd_Cast_04()
    {
        if (m_abilitiesReady[3])
        {
            m_nextAbilityReadyTime[3] = m_abilities[3].m_cooldown + Time.time;
            m_cooldownLeft[3] = m_abilities[3].m_cooldown;
            m_abilities[3].Initilise(m_abilities[3].m_projectilePrefab, m_projectileSpawn);
            m_abilities[3].TriggerAbility();
        }
    }

    private void Update()
    {
        //bool cooldownComplete = (Time.time > m_nextReadyTime);
        List<bool> cooldownsComplete = new List<bool>();
        Dictionary<int, bool> cooldowns = new Dictionary<int, bool>();

        //
        bool cooldownComplete1 = (Time.time > m_nextAbilityReadyTime[0]);
        bool cooldownComplete2 = (Time.time > m_nextAbilityReadyTime[1]);
        bool cooldownComplete3 = (Time.time > m_nextAbilityReadyTime[2]);
        //bool cooldownComplete4 = (Time.time > m_nextAbilityReadyTime[3]);

        //if (cooldownComplete)
        //{
        //    m_abilityReady = true;
        //    print("Ability ready");
        //}
        //else
        //{
        //    m_abilityReady = false;
        //    CoolDown(m_abilities[0].m_cooldown);
        //}

        print(m_cooldownLeft[0]);

        if (cooldownComplete1)
        {
            m_abilitiesReady[0] = true;
            print("Ability ready");
        }
        else
        {
            m_abilitiesReady[0] = false;
            CoolDown(0, m_abilities[0].m_cooldown);
        }

        if (cooldownComplete2)
        {
            m_abilitiesReady[1] = true;
            print("Ability ready");
        }
        else
        {
            m_abilitiesReady[1] = false;
            CoolDown(1, m_abilities[1].m_cooldown);
        }


        //for (int i = 0; i < m_nextAbilityReadyTime.Count; i++)
        //{
        //    bool cooldownComplete = (Time.time > m_nextAbilityReadyTime[i]);
        //    cooldownsComplete.Add(cooldownComplete);

        //    if (cooldownComplete)
        //    {
        //        m_abilitiesReady[i] = true;
        //        print("Ability ready");
        //    }
        //    else
        //    {
        //        m_abilitiesReady[i] = false;
        //        CoolDown(i, m_cooldownLeft[i], m_abilities[i].m_cooldown);
        //    }
        //}
    }


    IEnumerator Reload(float reloadTime)
    {
        m_isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        m_isReloading = false;
    }


    //void CoolDown(int index, float cooldownLeft, float cooldown)
    //{
    //    cooldownLeft -= Time.deltaTime;
    //    float roundedCD = Mathf.Round(cooldownLeft);
    //    UIManager.Instance.m_cooldownDisplayTexts[index].text = roundedCD.ToString();
    //    UIManager.Instance.m_darkMasks[index].fillAmount = (cooldownLeft / cooldown);
    //}

    void CoolDown(int index, float cooldown)
    {
        m_cooldownLeft[index] -= Time.deltaTime;
        float roundedCD = Mathf.Round(m_cooldownLeft[index]);
        UIManager.Instance.m_cooldownDisplayTexts[index].text = roundedCD.ToString();
        UIManager.Instance.m_darkMasks[index].fillAmount = (m_cooldownLeft[index] / cooldown);
    }





}
