using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Projectile_Snipe : Projectile
{

    Collider otherPlayer;

    public override void Launch()
    {
        //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
        Rigidbody projectileClone = Instantiate(m_prefab, m_spawnPos.position, m_spawnPos.rotation) as Rigidbody;
        projectileClone.rotation = m_spawnPos.rotation;

        if (projectileClone != null)
        {
            projectileClone.velocity = m_velocity;
            NetworkServer.Spawn(projectileClone.gameObject);
        }

    }


    private void Update()
    {
        CheckRange();
    }

    private void Start()
    {
        m_startLoc = m_spawnPos.position;
        CheckRange = CheckProjectileRange;
        //this.GetComponent<Rigidbody>().AddForce(m_spawnPos.forward * m_force);
    }

    public override void OnCollisionHit(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();

        if (player)
        {
            print("KILL");
            InstantiateFX(m_impactFX);
            player.Damage(m_damage, m_owner);
            Destroy(this.gameObject);
        }

    }

    // this makes sure that a projectile cannot hit it's owner.
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
