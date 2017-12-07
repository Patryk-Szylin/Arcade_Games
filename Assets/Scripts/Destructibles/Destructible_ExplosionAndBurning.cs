using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible_ExplosionAndBurning : MonoBehaviour
{
    public float m_health = 10f;
    public float m_radius = 5f;
    public float m_explosionDamage = 100f;

    public SO_Destructible_Base m_destroyEffect;

    private Player m_causedExplosionByPlayer;
    private Player m_playerAttacker;

    private void Start()
    {
        m_causedExplosionByPlayer = GetComponent<Player>();
        m_playerAttacker = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var projectile = other.GetComponent<Projectile>();

        if (projectile != null)
        {
            m_playerAttacker = projectile.m_owner;
            projectile.InstantiateFX(projectile.m_impactFX);
            TakeDamage(projectile.m_damage, m_playerAttacker);
        }
    }

    public void TakeDamage(float damage, Player player)
    {
        m_health -= damage;


        if(m_health <= 0 && m_destroyEffect != null)
        {
            m_causedExplosionByPlayer = player;
            print("Caused by : " + m_causedExplosionByPlayer.GetComponent<PlayerSetup>().m_playerName);

            var colliders = Physics.OverlapSphere(this.transform.position, m_radius);

            foreach (var nearbyObj in colliders)
            {
                var p = nearbyObj.GetComponent<PlayerHealth>();

                if (p != null)
                {
                    p.Damage(m_explosionDamage, m_causedExplosionByPlayer);
                }
            }

            StartCoroutine(m_destroyEffect.DestroyedEffect(this.gameObject));
        }
    }

}
