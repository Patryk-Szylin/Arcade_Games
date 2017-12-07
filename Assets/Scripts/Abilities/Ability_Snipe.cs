using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "Snipe Ability", menuName = "Abilities/Snipe")]
public class Ability_Snipe : Ability_Projectile
{

    [HideInInspector] public Projectile_Snipe m_launcher;

    [Header("Snipe Specific")]
    public float m_damage;

    public override string getToolTipStatInfo()
    {
        string newLine = "\n";

        return string.Format(
            "<size= 32> {0} </size>" + newLine
            + "<size= 24> {1} </size>" + newLine
            + "<size= 24> Cooldown : {2} </size>" + newLine
            + "<size= 24> Damage : <size= 26><color=red> {3} </color></size></size>" + newLine,
            m_name, m_description, m_cooldown, m_damage);
    }

    public override void Initilise(Transform playerGunPos, Vector3 mousePos)
    {
        m_launcher = m_projectilePrefab.GetComponent<Projectile_Snipe>();

        var dir = (mousePos - playerGunPos.position).normalized;

        // Projectile stats
        m_launcher.m_damage = m_damage;
        m_launcher.m_prefab = m_projectilePrefab;
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_missFX = m_missFX;
        m_launcher.m_range = m_range;
        m_launcher.m_force = m_force;
        m_launcher.m_velocity = dir * m_force;        
        m_launcher.m_spawnPos = playerGunPos;
        m_launcher.m_owner = getProjectileOwner(playerGunPos);
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
