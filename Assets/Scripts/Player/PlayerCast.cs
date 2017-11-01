using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{
    public List<MyAbility> m_abilityList = new List<MyAbility>();
    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;
    public bool m_isReloading = false;
    public float m_range = 50f;


    public void Cast()
    {

        CmdSpawnProjectile();
        StartCoroutine("Reload");
    }

    public void CastSpell_01()
    {
        var pc = GetPlayerOnHit(m_range);

        m_abilityList[0].m_player = pc;

        CmdCastSpell_1();
    }

    [Command]
    public void CmdCastSpell_1()
    {
        m_abilityList[0].ApplyToPlayer();
        Instantiate(m_abilityList[0].m_particleEffect, m_abilityList[0].m_player.transform.position, Quaternion.identity);
    }


    public void CastSpell_02()
    {
        var pc = GetPlayerOnHit(m_range);
        m_abilityList[1].ApplyTo(pc);
    }

    public void CastSpell_03()
    {
        m_abilityList[2].ApplyTo(this.GetComponent<PlayerController>());
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

    public PlayerController GetPlayerOnHit(float range)
    {
        var ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                var pc = hit.collider.GetComponent<PlayerController>();
                print("Player hit");
                return pc;
            }
        }

        return null;
    }
}
