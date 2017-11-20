using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class Projectile_DamageOverTime : Projectile
{
    [HideInInspector] public float m_damagePerTick;
    [HideInInspector] public float m_maxTicks;


    private MeshRenderer _sprites;
    private Collider _collider;

    private void OnEnable()
    {
        _sprites = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

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
        var ph = other.GetComponent<PlayerHealth>();
        if (ph)
            StartCoroutine(ApplyDoT(ph));        
    }

    public IEnumerator ApplyDoT(PlayerHealth playerhealth)
    {
        _sprites.enabled = false;
        _collider.enabled = false;

        for (int i = 0; i < m_maxTicks; i++)
        {
            if (playerhealth.m_isDead)
                break;

            playerhealth.Damage(m_damagePerTick, m_owner);
            yield return new WaitForSeconds(1f);
        }
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != m_owner)
        {
            OnCollisionHit(other);
        }
    }

}
