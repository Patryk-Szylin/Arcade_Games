using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Rocket : Projectile
{
    [HideInInspector] public float m_damage;
    [HideInInspector] public float m_radius;

    public override void Launch()
    {        
        Rigidbody rbody = Instantiate(m_prefab, m_spawnPos.position, m_spawnPos.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    public override void OnCollisionHit(Collider other)
    {
        var explosion = Instantiate(m_impactFX, transform.position, transform.rotation);
        NetworkServer.Spawn(explosion);


        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach(var nearbyObj in colliders)
        {
            // Check whether any colliders have destructible on them,
            // If so, damage them

            // Check if any of the colliders are players
            // If so, deal damage
            var player = nearbyObj.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.Damage(m_damage, m_owner);
            }
        }

        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }
}
