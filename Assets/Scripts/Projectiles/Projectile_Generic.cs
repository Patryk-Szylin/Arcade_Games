using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile_Generic : Projectile
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
        throw new System.NotImplementedException();
    }
}
