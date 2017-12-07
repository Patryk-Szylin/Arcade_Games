using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Linq;

public class Projectile_DamageOverTime : Projectile
{
    [HideInInspector] public float m_maxTicks;


    private MeshRenderer _sprites;
    private Collider _collider;

    Collider otherPlayer;

    private void OnEnable()
    {
        _sprites = this.GetComponent<MeshRenderer>();
        _collider = this.GetComponent<Collider>();
    }

    private void Start()
    {
        m_startLoc = m_spawnPos.position;
        CheckRange = CheckProjectileRange;
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

    private void Update()
    {
        CheckRange();
    }


    public override void OnCollisionHit(Collider other)
    {
        var ph = other.GetComponent<PlayerHealth>();

        if (ph != null)
        {

            // If the projectile collided, don't check for range anymore

            CheckRange = () => { };
            InstantiateFX(m_impactFX);
            RpcHideProjectile();
            StartCoroutine(ApplyDoT(ph));
        }


    }

    public IEnumerator ApplyDoT(PlayerHealth playerhealth)
    {
        for (int i = 0; i < m_maxTicks; i++)
        {
            //CmdHideProjectile();
            print("APPLYING DOT");

            if (playerhealth.m_isDead)
                break;

            playerhealth.Damage(m_damage, m_owner);
            yield return new WaitForSeconds(1f);
        }

        Destroy(this.gameObject);
    }

    //[Command]
    [ClientRpc]
    void RpcHideProjectile()
    {
        var sprite = this.GetComponent<MeshRenderer>();
        var collider = this.GetComponent<Collider>();

        sprite.enabled = false;
        collider.enabled = false;


        //_sprites.enabled = false;
        //_collider.enabled = false;
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
