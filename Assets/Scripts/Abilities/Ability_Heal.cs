using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Abilities/Heal Bullet", fileName = "Heal Bullet")]
public class Ability_Heal : Ability_Projectile
{
    [HideInInspector] public Projectile_Heal m_launcher;


    [Header("Heal Specific")]
    public float m_healAmount;

    public override string getToolTipStatInfo()
    {
        string newLine = "\n";

        return string.Format(
            "<size= 32> {0} </size>" + newLine
            + "<size= 24> {1} </size>" + newLine
            + "<size= 24> Cooldown : {2} </size>" + newLine
            + "<size= 24> Heal Amount : <size= 14><color=green> {3} </color></size></size>",
            m_name, m_description, m_cooldown, m_healAmount);
    }

    public override void Initilise(Transform PlayerGunPos, Vector3 mousePos)
    {
        //var destination = GetAbilityPointInWorldSpace();
        var dir = (mousePos - PlayerGunPos.position).normalized;

        m_launcher = m_projectilePrefab.GetComponent<Projectile_Heal>();
        m_launcher.m_healAmount = m_healAmount;
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_missFX = m_missFX;
        m_launcher.m_prefab = m_projectilePrefab;
        m_launcher.m_spawnPos = PlayerGunPos;
        m_launcher.m_velocity = dir * m_force;
        m_launcher.m_range = m_range;
        m_launcher.m_owner = getProjectileOwner(PlayerGunPos);
    }

    public override void Initilise()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }
}
