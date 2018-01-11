using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This ability has small cooldown between shots, it's like semi-automatic machine gun

[CreateAssetMenu(fileName = "Machine Gun", menuName ="Abilities/MachineGun")]
public class Ability_MachineGun : Ability_Projectile
{

    [HideInInspector] public Projectile_MachineGun m_launcher;
    public int m_bulletsPerRound;


    public override string getToolTipStatInfo()
    {
        return "";
    }

    public override void Initilise(Transform playerGunPos, Vector3 mousePos)
    {
        //var destination = GetAbilityPointInWorldSpace();
        var dir = (mousePos - playerGunPos.position).normalized;

        m_launcher = m_projectilePrefab.GetComponent<Projectile_MachineGun>();
        m_launcher.m_impactFX = m_impactFX;
        m_launcher.m_missFX = m_missFX;
        m_launcher.m_prefab = m_projectilePrefab;
        m_launcher.m_spawnPos = playerGunPos;
        m_launcher.m_velocity = dir * m_force;
        m_launcher.m_bulletsPerRound = m_bulletsPerRound;
        m_launcher.m_range = m_range;

        m_launcher.m_owner = getProjectileOwner(playerGunPos);
    }

    public override void Initilise()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerAbility()
    {
        //m_launcher.Launch();
        DischargeBullets();
    }

    public void DischargeBullets()
    {
        for (int i = 0; i < m_bulletsPerRound; i++)
        {
            m_launcher.Launch();
        }
    }
}
