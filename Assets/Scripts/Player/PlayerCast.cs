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
    public float m_reloadTime = 1f;         // needs to be moved to an ability
    public bool m_isReloading = false;
    

    public void Cast()
    {
        //CmdSpawnProjectile();

        StartCoroutine("Reload");
    }

    [Command]
    public void Cmd_Cast_01()
    {
        m_abilities[0].Initilise(m_abilities[0].m_bulletPrefab, m_projectileSpawn);
        m_abilities[0].TriggerAbility();
    }

    [Command]
    public void Cmd_Cast_02()
    {
        m_abilities[1].Initilise(m_abilities[1].m_bulletPrefab, m_projectileSpawn);
        m_abilities[1].TriggerAbility();
    }


    //// Needs to be a COMMAND so server can spawn it
    //[Command]
    //private void CmdSpawnProjectile()
    //{
    //    Projectile projectile = null;
    //    projectile = m_projectilePrefab.GetComponent<Projectile>();

    //    Rigidbody rbody = Instantiate(m_projectilePrefab, m_projectileSpawn.position, m_projectileSpawn.rotation) as Rigidbody;

    //    if (rbody != null)
    //    {
    //        rbody.velocity = projectile.m_speed * m_projectileSpawn.transform.forward;
    //        NetworkServer.Spawn(rbody.gameObject);
    //    }
    //}

    IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(m_reloadTime);
        m_isReloading = false;
    }





}
