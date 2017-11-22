using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile_Heal : Projectile
{
    [HideInInspector] public float m_healAmount;

    public override void Launch()
    {
        Rigidbody rbody = Instantiate(m_prefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    public override void OnCollisionHit(Collider other)
    { 
        if (other.tag == "Enemy" || other.tag == "Object")
        {
            //explosion();

            if (other.tag == "Object")
            {
                Destroy(this.gameObject);
            }

            if (other.tag == "Enemy" || other.tag == "Player")
            {
                // Play healing effect
                var player = other.gameObject.GetComponent<PlayerHealth>();

                // Otherwise, keep adding health
                CmdPlayerHeal(other.name, m_healAmount, sourceID);              
            }

            // Destoy projectile
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }

    [Command]
    void CmdPlayerHeal(string playerID, float heal, string sourceID)
    {
        Player player = GameManager.GetPlayer(playerID);
        player.GetComponent<PlayerHealth>().RpcHeal(heal, sourceID);
    }
}
