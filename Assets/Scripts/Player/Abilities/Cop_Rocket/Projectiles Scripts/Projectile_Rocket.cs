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
        var explosion = Instantiate(m_impactFX, transform.position, transform.rotation);
        NetworkServer.Spawn(explosion);


        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        print(colliders.Length);

        foreach (var nearbyObj in colliders)
        {
            // Check whether any colliders have destructible on them,
            // If so, damage them

            // Check if any of the colliders are players
            // If so, deal damage
            var player = nearbyObj.GetComponent<PlayerHealth>();
            if (player != null)
            {
                //player.Damage(damage, m_owner);
                CmdPlayerDamage(other.name, damage, sourceID);
            }
        }

        Destroy(this.gameObject);




        //print(other.gameObject);

        //if (other.tag == "Enemy" || other.tag == "Object" || other.tag == "Player")
        //{
        //    explosion();

        //    if (other.tag == "Object")
        //    {
        //        Destroy(this.gameObject);
        //    }

        //    if (other.tag == "Enemy" || other.tag == "Player")
        //    {
        //        CmdPlayerDamage(other.name, damage, sourceID);
        //    }

        //    Destroy(this.gameObject);
        //}
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
