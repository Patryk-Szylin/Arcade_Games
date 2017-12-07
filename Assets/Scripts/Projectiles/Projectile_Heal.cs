using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile_Heal : Projectile
{
    [HideInInspector] public float m_healAmount;

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
        CheckRange = CheckProjectileRange;

        //this.GetComponent<Rigidbody>().AddForce(m_spawnPos.forward * m_force);
    }

    public override void OnCollisionHit(Collider other)
    {
        // Play healing effect

        var player = other.gameObject.GetComponent<PlayerHealth>();

        if(player != null)
        {
            // Making sure that player's current health can never exceed their max health capacity
            if(player.m_currentHealth < player.m_maxHealth)
            {
                // Calculate how much health is missing
                var diff = player.m_maxHealth - player.m_currentHealth;
                print(diff);

                // Check if difference is less than m_healAmount
                if (diff < m_healAmount)
                {
                    // If so, just add the difference to current hp
                    player.m_currentHealth += diff;
                    return;
                }                   

                // Otherwise, keep adding health
                player.m_currentHealth += m_healAmount;                   
            }                

            // Destoy projectile
            Destroy(this.gameObject);
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.GetComponent<Player>() != m_owner)
    //    {
    //        print("EXECUTING HEALIGN");
    //        OnCollisionHit(other);
    //    }
    //}

    private void Update()
    {
        CheckRange();
    }




    [Command]
    void CmdCheckForCollision()
    {
        if (otherPlayer.GetComponent<Player>() != m_owner)
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
