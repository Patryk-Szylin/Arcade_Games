using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Rocket : Projectile
{
    public override void Launch()
    {        
        Rigidbody rbody = Instantiate(m_prefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    void explosion()
    {
        GameObject projectileExplosion = Instantiate(m_impactFX, transform.position, transform.rotation);
    }

    public override void OnCollisionHit(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Object")
        {
            explosion();

            if (other.tag == "Object")
            {
                Destroy(this.gameObject);
            }

            if (other.tag == "Enemy" || other.tag == "Player")
            {
                CmdPlayerDamage(other.name, damage, sourceID);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }

    [Command]
    void CmdPlayerDamage(string playerID, float dmg, string sourceID)
    {
        Debug.Log(sourceID + " Hit" + playerID);
        Player player = GameManager.GetPlayer(playerID);
        player.GetComponent<PlayerHealth>().RpcDamage(dmg, sourceID);
    }
}
