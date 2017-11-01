using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{    
    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;
    public bool m_isReloading = false;
    

    public void Cast()
    {
        CmdSpawnProjectile();

        StartCoroutine("Reload");
    }

    // Needs to be a COMMAND so server can spawn it
    [Command]
    private void CmdSpawnProjectile()
    {
        Projectile projectile = null;
        projectile = m_projectilePrefab.GetComponent<Projectile>();

        Rigidbody rbody = Instantiate(m_projectilePrefab, m_projectileSpawn.position, m_projectileSpawn.rotation) as Rigidbody;

        if (rbody != null)
        {
            rbody.velocity = projectile.m_speed * m_projectileSpawn.transform.forward;
            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(m_reloadTime);
        m_isReloading = false;
    }





}
