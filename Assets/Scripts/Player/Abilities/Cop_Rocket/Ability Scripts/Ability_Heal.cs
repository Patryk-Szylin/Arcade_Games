using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName ="Abilities/Heal Bullet", fileName = "Heal Bullet")]
public class Ability_Heal : AbilityProjectile
{    
    [HideInInspector] public Projectile_Heal m_launcher;

    [Header("Heal Specific")]
    public float m_healAmount;

    public override void Initilise(Transform playerGunPos, string sourceID)
    {
        var destination = GetAbilityPointInWorldSpace();
        var dir = (destination - playerGunPos.position).normalized;

        m_launcher = projectilePrefab.GetComponent<Projectile_Heal>();
        m_launcher.m_healAmount = m_healAmount;
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_prefab = projectilePrefab;
        m_launcher.projectileSpawnLocation = playerGunPos;
        m_launcher.m_velocity = dir * projectileForce;
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }
}
