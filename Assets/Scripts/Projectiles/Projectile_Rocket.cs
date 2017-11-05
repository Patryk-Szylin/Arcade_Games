using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Rocket : Projectile
{
    public override void Launch()
    {        
        Rigidbody rbody = Instantiate(m_prefab, m_spawnPos.position, m_spawnPos.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    public override void OnCollisionHit(Collision collision)
    {
        var explosion = Instantiate(m_explosionFX, transform.position, transform.rotation);
        NetworkServer.Spawn(explosion);


        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach(var nearbyObj in colliders)
        {
            // Check whether any colliders have destructible on them,
            // If so, damage them

            // Check if any of the colliders are players
            // If so, take damage
            var player = nearbyObj.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.m_currentHealth -= m_damage;
            }
        }

        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionHit(collision);
    }
}
