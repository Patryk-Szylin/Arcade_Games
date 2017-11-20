using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{
    // CONSTANTS
    public const int MAX_ABILITY_COUNT = 4;


    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;
    public List<Ability> m_abilities = new List<Ability>();

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;                     // needs to be moved to an ability
    public bool m_isReloading = false;


    //=========
    private float m_nextReadyTime;

    public List<bool> m_abilitiesReady = new List<bool>();
    public List<float> m_nextAbilityReadyTime = new List<float>();
    public List<float> m_cooldownLeft = new List<float>();
    public List<Sprite> m_abilitySprites = new List<Sprite>();
    public Sprite m_noAbilitySprite;


    private void Start()
    {
        if (isLocalPlayer)
        {
            for (int i = 0; i < 4; i++)
            {
                if (m_abilities[i] != null)
                {
                    m_abilitiesReady.Add(true);
                    m_nextAbilityReadyTime.Add(0);
                    m_cooldownLeft.Add(m_abilities[i].m_cooldown);
                    m_abilitySprites.Add(m_abilities[i].m_abilityIcon);
                }
                else
                {
                    m_abilitiesReady.Add(false);
                    m_nextAbilityReadyTime.Add(0);
                    m_cooldownLeft.Add(0);
                    m_abilitySprites.Add(m_noAbilitySprite);
                }                    
            }

            if (m_abilities[3] == null)
            {
                UIManager.Instance.m_abilitySprites[3].sprite = m_noAbilitySprite;
            }
        }


    }

    [Command]
    public void Cmd_Cast(int abilityIndex, Vector3 direction)
    {
        m_abilities[abilityIndex].Initilise(m_abilities[abilityIndex].m_projectilePrefab, m_projectileSpawn, direction);
        m_abilities[abilityIndex].TriggerAbility();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            UpdateCooldownUI(0);
            UpdateCooldownUI(1);
            UpdateCooldownUI(2);

            if (m_abilities[3])
            {
                UpdateCooldownUI(3);
            }
            
        }
    }

    public void CastAbility(int index)
    {
        // UI STUFF
        if (m_abilitiesReady[index] && m_abilities[index]  != null)
        {
            m_nextAbilityReadyTime[index] = m_abilities[index].m_cooldown + Time.time;
            m_cooldownLeft[index] = m_abilities[index].m_cooldown;

            Vector3 castDirection = GetAbilityPointInWorldSpace();


            // Re-enable ui
            AbilityReady(index, true);
            Cmd_Cast(index, castDirection);
        }
    }

    void UpdateCooldownUI(int index)
    {
        if (!isLocalPlayer)
            return;

        //print(UIManager.Instance.m_abilityIcons.Count);

        UIManager.Instance.m_abilitySprites[index].sprite = m_abilities[index].m_abilityIcon;
        UpdateToolTipUI(index);

        

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


    void UpdateToolTipUI(int index)
    {
        UIManager.Instance.m_abilityTooltipObjects[index].text = m_abilities[index].getToolTipStatInfo();
    }

    void CoolDown(int index, float cooldown)
    {
        if (isLocalPlayer)
        {
            m_cooldownLeft[index] -= Time.deltaTime;
            float roundedCD = Mathf.Round(m_cooldownLeft[index]);

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

    // This should be in utility class
    public Vector3 GetAbilityPointInWorldSpace()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var rayWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 99999f))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

}
