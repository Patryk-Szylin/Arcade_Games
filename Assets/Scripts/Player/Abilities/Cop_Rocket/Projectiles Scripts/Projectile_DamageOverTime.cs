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
        Rigidbody rbody = Instantiate(m_prefab, projectileSpawnLocation.position, projectileSpawnLocation.rotation) as Rigidbody;

        if (rbody != null)
        {
            rbody.velocity = m_velocity;

            NetworkServer.Spawn(rbody.gameObject);
        }
    }

    void explosion()
    {
        GameObject projectileExplosion = Instantiate(m_impactFX, transform.position, transform.rotation);
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
                StartCoroutine(ApplyDoT(other.gameObject));
            }
        }  
    }

    public IEnumerator ApplyDoT(GameObject player)
    {
        _sprites.enabled = false;
        _collider.enabled = false;

        for (int i = 0; i < m_maxTicks; i++)
        {
            CmdPlayerDamage(player.name, m_damagePerTick, sourceID);
            yield return new WaitForSeconds(1f);
        }
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        OnCollisionHit(other);
    }

    [Command]
    void CmdPlayerDamage(string playerID, float dmg, string sourceID)
    {
        Debug.Log(sourceID + " Hit" + playerID);
        Player player = GameManager.GetPlayer(playerID);
        player.GetComponent<PlayerHealth>().RpcDamage(dmg, sourceID);
    }
}
