using UnityEngine;
using System.Collections;

public abstract class Ability_Projectile : Ability
{
    [Header("Projectile Specific")]
    [HideInInspector] public Vector3 m_direction;
    public Rigidbody m_projectilePrefab;
    public float m_force;
    public float m_range;


    public Player getProjectileOwner(Transform fromGunPos)
    {
        return fromGunPos.GetComponentInParent<Player>();
    }

    public override Rigidbody getProjectilePrefab()
    {
        return m_projectilePrefab;
    }
}
