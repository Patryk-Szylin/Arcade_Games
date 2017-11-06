using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Abilities/Heal Bullet", fileName = "Heal Bullet")]
public class Ability_Heal : Ability
{    
    [HideInInspector] public Projectile_Heal m_launcher;


    [Header("Heal Specific")]
    public float m_healAmount;

    public override void Initilise(Rigidbody projectileObj, Transform PlayerGunPos)
    {
        var destination = GetAbilityPointInWorldSpace();
        var dir = (destination - PlayerGunPos.position).normalized;

        m_launcher = projectileObj.GetComponent<Projectile_Heal>();
        m_launcher.m_healAmount = m_healAmount;
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_prefab = m_bulletPrefab;
        m_launcher.m_spawnPos = PlayerGunPos;
        m_launcher.m_velocity = dir * m_force;
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }
}
