using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{
    // CONSTANTS
    public const int MAX_ABILITY_COUNT = 5;

    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;
    public List<Ability> m_abilities = new List<Ability>();

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;                     // needs to be moved to an ability
    public bool m_isReloading = false;

    public List<bool> m_abilitiesReady = new List<bool>();
    public List<float> m_nextAbilityReadyTime = new List<float>();
    public List<float> m_cooldownLeft = new List<float>();
    public List<Sprite> m_abilitySprites = new List<Sprite>();
    public Sprite m_noAbilitySprite;

    private float m_nextReadyTime;

    private void Start()
    {
        if (isLocalPlayer)
        {
            for (int i = 0; i < MAX_ABILITY_COUNT; i++)
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

            // Check for last ability (pickable ability/gun)
            if (m_abilities[MAX_ABILITY_COUNT - 1] == null)
            {
                UIManager.Instance.m_abilitySprites[MAX_ABILITY_COUNT - 1].sprite = m_noAbilitySprite;
            }
        }


    }

    [Command]
    public void Cmd_Cast(int abilityIndex, Vector3 mousePos)
    {
        m_abilities[abilityIndex].Initilise(m_projectileSpawn, mousePos);
        m_abilities[abilityIndex].TriggerAbility();
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            UpdateCooldownUI(0);
            UpdateCooldownUI(1);
            UpdateCooldownUI(2);
            UpdateCooldownUI(3);


            // Initilly when players start game, there's no 4th ability. So only update ui if there's one.
            if (m_abilities[MAX_ABILITY_COUNT - 1])
            {
                UpdateCooldownUI(MAX_ABILITY_COUNT - 1);
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

            // Re-enable ui
            AbilityReady(index, true);

            Vector3 mousePos = GetMousePointInScreenSpace();
            var targetDir = (mousePos - m_projectileSpawn.position).normalized;
            m_projectileSpawn.transform.LookAt(mousePos);

            Cmd_Cast(index, mousePos);
        }
    }

    void UpdateCooldownUI(int index)
    {
        if (!isLocalPlayer)
            return;

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
    public Vector3 GetMousePointInScreenSpace()
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

    public int GetMaxAbilityCount()
    {
        return MAX_ABILITY_COUNT;
    }

    public void Reset()
    {
        m_abilities[MAX_ABILITY_COUNT - 1] = null;
        UIManager.Instance.m_abilitySprites[MAX_ABILITY_COUNT - 1].sprite = m_noAbilitySprite;
    }

}
