using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desctructibles : MonoBehaviour
{
    public float m_health = 50f;

    public Desctructible_Effect m_destroyEffect;


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();

        var projectile = other.GetComponent<Projectile>();

        if(projectile != null)
        {
            TakeDamage(projectile.m_damage);
        }


    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;

        if(m_health <= 0 && m_destroyEffect != null)
        {
            StartCoroutine(m_destroyEffect.DestroyedEffect(this.gameObject));
        }
    }


}
