using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Heal : Ability<Projectile_Heal>
{
    [HideInInspector] public float m_healAmount;

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
