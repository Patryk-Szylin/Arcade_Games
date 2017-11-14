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
/// Ideas:                      Using this class, an user can create two assets. One for damaging over time, and one to heal over time by setting
///                             <para m_damagePerTick> to be e.g. -10. This in in turn will add health to the player instead of taking it.
/// 
/// </summary>


[CreateAssetMenu(menuName = "Abilities/Damage Over Time", fileName = "DoT")]
public class Ability_DamageOverTime : Ability
{
    [HideInInspector] public Projectile_DamageOverTime m_launcher;

    [Header("Dot Specific")]
    public float m_damagePerTick;
    public float m_maxTicks;

    public override string getToolTipStatInfo()
    {
        string newLine = "\n";

        return string.Format(
            "<size= 16> {0} </size>" + newLine
            + "<size= 12> {1} </size>" + newLine
            + "<size= 12> Cooldown : {2} </size>" + newLine
            + "<size= 12> Damage per tick : <size= 14><color=red> {3} </color></size></size>" + newLine
            + "<size= 12> Max Ticks : {4} </size>",
            m_name, m_description, m_cooldown, m_damagePerTick, m_maxTicks);
    }

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
