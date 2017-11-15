using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Ability_Sniper_AA : Projectile
{
    public override void Launch()
    {
        startLocation = projectileSpawnLocation.position;
        //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
        Rigidbody projectileClone = Instantiate(m_prefab, projectileSpawnLocation.position, transform.rotation) as Rigidbody;

        if (projectileClone != null)
        {
            //Add force to the instantiated bullet, pushing it forward away from the bulletSpawn location, 
            // using projectile force for how hard to push it away
            projectileClone.AddForce(projectileSpawnLocation.transform.right * projectileForce);

            NetworkServer.Spawn(projectileClone.gameObject);
        }
    }

    void Update()
    {
        // Range
        if (Vector3.Distance(transform.position, startLocation) > range)
        {
            explosion();
            Destroy(this.gameObject);
        }
    }

    void explosion()
    {
        GameObject projectileExplosion = Instantiate(m_impactFX, transform.position, transform.rotation);
    }

    public override void OnCollisionHit(Collider other)
    {
        if (other.tag != "Player")
        {
            explosion();

            if (other.tag == "Enemy")
            {
                PlayerHealth enemyHealth = other.GetComponent<PlayerHealth>();
                if (enemyHealth != null)
                {
                    //enemyHealth.RpcDamage(damage);
                }
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }
}
