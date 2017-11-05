using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Generic")]
public class Ability_Generic : Ability
{
    public override void Initilise(Rigidbody projectileObj, Transform playerGunPos)
    {
        var dest = GetAbilityPointInWorldSpace();
        var dir = (dest - playerGunPos.position).normalized;

        m_launcher = projectileObj.GetComponent<Projectile>();
        m_launcher.m_damage = 0;
        m_launcher.m_velocity = dir * m_force;
        m_launcher.m_prefab = m_bulletPrefab;
        m_launcher.m_spawnPos = playerGunPos;
        m_launcher.m_explosionFX = m_explosionFX;
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }
}
