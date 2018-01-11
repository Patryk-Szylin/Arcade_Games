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
    public Transform m_projectileDebug;
    public List<Ability> m_abilities = new List<Ability>();

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;                     // needs to be moved to an ability
    public bool m_isReloading = false;


    // THESE ARE CREATED IN THE INSPECTOR, each have 5 elements.
    public List<bool> m_abilitiesReady = new List<bool>();
    public List<float> m_nextAbilityReadyTime = new List<float>();
    public List<float> m_cooldownLeft = new List<float>();
    public List<Sprite> m_abilitySprites = new List<Sprite>();
    public Sprite m_noAbilitySprite;

    private void Start()
    {
        if (isLocalPlayer)
        {
            // Check for last ability (pickable ability/gun)
            if (m_abilities[MAX_ABILITY_COUNT - 1] == null)
            {
                UIManager.Instance.m_abilitySprites[MAX_ABILITY_COUNT - 1].sprite = m_noAbilitySprite;
            }
        }
    }

    public void EquipNewAbility(Ability newAbility)
    {
        m_abilities[4] = newAbility;
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
            for (int i = 0; i < MAX_ABILITY_COUNT - 1; i++)
            {
                UpdateCooldownUI(i);
            }

            // Initilly when players start game, there's no 4th ability. So only update ui if there's one.
            if (m_abilities[MAX_ABILITY_COUNT - 1])
            {
                UpdateCooldownUI(MAX_ABILITY_COUNT - 1);
            }

            // ProjectileSpawnLocation Rotation
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 mouseLocation;
            if (Physics.Raycast(ray, out hit, 100))
            {
                mouseLocation = hit.point;
                Vector3 targetDir = mouseLocation - transform.position;
                float angle = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg;
                if(m_projectileDebug != null)
                {
                    m_projectileDebug.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
                }
                
            }
        }
    }

    public void CastAbility(int index)
    {
        // UI STUFF
        if (m_abilitiesReady[index] && m_abilities[index] != null)
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
