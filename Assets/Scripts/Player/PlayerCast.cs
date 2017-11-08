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


    PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void Cast()
    {
        //CmdSpawnProjectile();

        StartCoroutine("Reload");
    }

    [Command]
    public void Cmd_Cast_01()
    {
        m_abilities[0].Initilise(m_abilities[0].m_projectilePrefab, m_projectileSpawn);
        m_abilities[0].TriggerAbility();
    }

    [Command]
    public void Cmd_Cast_02()
    {
        m_abilities[1].Initilise(m_abilities[1].m_projectilePrefab, m_projectileSpawn);
        m_abilities[1].TriggerAbility();
    }

    [Command]
    public void Cmd_Cast_03()
    {
        m_abilities[2].Initilise(m_abilities[2].m_projectilePrefab, m_projectileSpawn);
        m_abilities[2].TriggerAbility();
    }


    IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(m_reloadTime);
        m_isReloading = false;
    }





}
