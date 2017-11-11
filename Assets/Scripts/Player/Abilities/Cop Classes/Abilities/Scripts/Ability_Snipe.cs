using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Snipe", fileName = "Snipe")]
public class Ability_Snipe : AbilityProjectile
{
    [HideInInspector] public Projectile launcher;

    [Header("Snipe Variables")]
    public float damage;
    public float range;
    public float blastRadius;

    public override void Initilise()
    {
        Vector3 direction = GetAbilityPointInWorldSpace();
        launcher = projectilePrefab.GetComponent<Projectile>();

        // Projectile Stats
        launcher.damage = damage;
        launcher.range = range;
        launcher.projectileForce = projectileForce;
        launcher.blastRadius = blastRadius;

        // ----
        launcher.m_prefab = projectilePrefab;
        launcher.projectileSpawnLocation = projectileSpawnLocation;
        launcher.m_impactFX = m_impactFX;
    }

    public override void TriggerAbility()
    {
        launcher.Launch();
    }
}
