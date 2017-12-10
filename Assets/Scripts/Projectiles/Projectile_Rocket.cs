using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Rocket : Projectile
{
    [HideInInspector] public float m_radius;

    Collider otherPlayer;

    public override void Launch()
    {
        Rigidbody rbody = Instantiate(m_prefab, m_spawnPos.position, m_spawnPos.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    private void Start()
    {
        m_startLoc = m_spawnPos.position;
        //CheckRange = CheckProjectileRange;
    }

    public override void OnCollisionHit(Collider other)
    {
        var explosion = Instantiate(m_impactFX, transform.position, transform.rotation);
        CameraScript.Instance.CameraShake(); //TODO: Only shake on players near by!!
        NetworkServer.Spawn(explosion);


        Collider[] colliders = Physics.OverlapSphere(transform.position, m_radius);

        foreach (var nearbyObj in colliders)
        {
            // Check whether any colliders have destructible on them,
            // If so, damage them
            var destructible = nearbyObj.GetComponent<Destructible_ExplosionAndBurning>();
            if(destructible != null)
            {
                destructible.TakeDamage(m_damage, m_owner);
            }

            // Check if any of the colliders are players
            // If so, deal damage
            var player = nearbyObj.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.Damage(m_damage, m_owner);
            }
        }

        Destroy(this.gameObject);
    }

    
    [Command]
    void CmdCheckForCollision()
    {
        if(otherPlayer.GetComponent<Player>() != m_owner)
        {
            OnCollisionHit(otherPlayer);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        otherPlayer = other;
        CmdCheckForCollision();
    }
}
