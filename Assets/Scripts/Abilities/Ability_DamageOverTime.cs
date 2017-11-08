using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Description:                The classes uses an attribute "CreateAssetMenu", this is used in the inspector whenever user wants to create new asset.
///                             From menu -> Assets -> Create -> "Abilities"/// 
/// 
///                             During initialisation process the m_launcher reference is used to set appropriate values coming from the asset.
///                             It also gets position of the mouse and sets the direction of the projectile before instantiating it.                            
///                             
///
/// <param name="m_launcher">   A projectile reference. Since the ability is "Damage over Time", it uses projectile for DoT damage.
///
/// Remaining params are self-explanatory.
/// 
/// </summary>


[CreateAssetMenu(menuName = "Abilities/Damage Over Time", fileName = "DoT")]
public class Ability_DamageOverTime : Ability
{
    [HideInInspector] public Projectile_DamageOverTime m_launcher;

    [Header("Dot Specific")]
    public float m_damagePerTick;
    public float m_maxTicks;

    public override void Initilise(Rigidbody targetObj, Transform PlayerGunPos)
    {
        var destination = GetAbilityPointInWorldSpace();
        var dir = (destination - PlayerGunPos.position).normalized;

        m_launcher = targetObj.GetComponent<Projectile_DamageOverTime>();
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_prefab = m_projectilePrefab;
        m_launcher.m_damagePerTick = m_damagePerTick;
        m_launcher.m_maxTicks = m_maxTicks;
        m_launcher.m_spawnPos = PlayerGunPos;
        m_launcher.m_velocity = dir * m_force;
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }


}
