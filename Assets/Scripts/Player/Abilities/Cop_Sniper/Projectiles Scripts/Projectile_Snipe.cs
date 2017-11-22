using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Projectile_Snipe : Projectile
{
    public override void Launch()
    {
        //Instantiate a copy of our projectile and store it in a new rigidbody variable called clonedBullet
        Rigidbody projectileClone = Instantiate(m_prefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation) as Rigidbody;
        projectileClone.rotation = projectileSpawnLocation.rotation;

        NetworkServer.Spawn(projectileClone.gameObject);
    }

    void Start()
    {
        startLocation = gameObject.GetComponent<Transform>().position;
        Rigidbody test = gameObject.GetComponent<Rigidbody>();
        test.AddForce(transform.right * projectileForce);
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
        //if (other.tag != "Player")
        if (other.tag == "Enemy" || other.tag == "Player" || other.tag == "Object")
        {
            explosion();

            if (other.tag == "Object")
            {
                Destroy(this.gameObject);
            }

            if (other.tag == "Enemy" || other.tag == "Player")
            {
                GameObject playerID = GameObject.FindGameObjectWithTag("Player");
                CmdPlayerDamage(other.name, damage, sourceID);
                Debug.Log(playerID.name);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }

    [Command]
    void CmdPlayerDamage (string playerID, float dmg, string sourceID)
    {
        Debug.Log(sourceID + " Hit" + playerID);
        Player player = GameManager.GetPlayer(playerID);
        player.GetComponent<PlayerHealth>().RpcDamage(dmg, sourceID);
    }
}
