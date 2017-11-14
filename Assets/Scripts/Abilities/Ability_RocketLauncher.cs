﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Rocket Launcher", fileName = "Rocket Launcher")]
public class Ability_RocketLauncher : Ability
{
    [HideInInspector] public Vector3 m_destination;
    [HideInInspector] public Projectile_Rocket m_launcher;

    [Header("Rocket Specific")]
    public float m_radius;
    public float m_damage;

    public override void Initilise(Rigidbody targetObj, Transform playerGunPos)
    {
        var dest = GetAbilityPointInWorldSpace();
        SetRocketDestination(dest);
        var velocity = BallisticVelocity(m_destination, 45f, playerGunPos.position);

        m_launcher = targetObj.GetComponent<Projectile_Rocket>();
        m_launcher.m_damage = m_damage;
        m_launcher.m_velocity = velocity;
        m_launcher.m_prefab = m_projectilePrefab;
        m_launcher.m_spawnPos = playerGunPos;
        m_launcher.m_radius = m_radius;
        m_launcher.m_impactFX = m_impactFX;
    }

    public override void TriggerAbility()
    {
        m_launcher.Launch();
    }

    public void SetRocketDestination(Vector3 point)
    {
        m_destination = point;
    }

    private Vector3 BallisticVelocity(Vector3 destination, float angle, Vector3 originPos)
    {
        Vector3 dir = destination - originPos; // get Target Direction
        float height = dir.y; // get height difference
        dir.y = 0; // retain only the horizontal difference
        float dist = dir.magnitude; // get horizontal direction
        float a = angle * Mathf.Deg2Rad; // Convert angle to radians
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
        dist += height / Mathf.Tan(a); // Correction for small height differences

        // Calculate the velocity magnitude
        float velocity = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * dir.normalized; // Return a normalized vector.
    }

    public override string getToolTipStatInfo()
    {
        string newLine = "\n";

        return string.Format(
            "<size= 16> {0} </size>" + newLine
            + "<size= 12> {1} </size>" + newLine
            + "<size= 12> Cooldown : {2} </size>" + newLine
            + "<size= 12> Damage : <size= 14><color=red> {3} </color></size></size>" + newLine
            + "<size= 12> Radius : {4} </size>",
            m_name, m_description, m_cooldown, m_damage, m_radius);
    }
}
