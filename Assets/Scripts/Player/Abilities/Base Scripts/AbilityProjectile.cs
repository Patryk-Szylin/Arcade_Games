using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityProjectile : Ability
{
    [Header("Projectile Settings")]
    public Rigidbody projectilePrefab;
    public float projectileForce;

    [Header("Ability Effects")]
    public GameObject m_abilityFX;      // For shooting/firing effect
    public GameObject m_impactFX;       // e.g. Explosion
    public GameObject m_trailFX = null; // Used if there's a trail 

    public override void TriggerAbility() { }
    public override void Initilise(Transform projectileSpawnLocation, string souceID, Vector3 direction) { }
}
