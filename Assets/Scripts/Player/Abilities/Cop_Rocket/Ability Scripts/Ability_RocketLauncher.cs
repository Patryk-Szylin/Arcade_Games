using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Rocket Launcher", fileName = "Rocket Launcher")]
public class Ability_RocketLauncher : AbilityProjectile
{
    [HideInInspector] public Vector3 m_destination;
    [HideInInspector] public Projectile_Rocket m_launcher;

    [Header("Rocket Specific")]
    public float m_radius;
    public float m_damage;
    public float blastRadius;

    public override void Initilise(Transform playerGunPos, string sourceID, Vector3 direction)
    {
        var dest = direction;
        SetRocketDestination(dest);
        var velocity = BallisticVelocity(m_destination, 45f, playerGunPos.position);

        m_launcher = projectilePrefab.GetComponent<Projectile_Rocket>();
        m_launcher.damage = m_damage;
        m_launcher.m_velocity = velocity;
        m_launcher.m_prefab = projectilePrefab;
        m_launcher.projectileSpawnLocation = playerGunPos;
        m_launcher.blastRadius = m_radius;
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

}
