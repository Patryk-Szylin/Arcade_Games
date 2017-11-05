using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/Rocket Launcher", fileName = "Rocket Launcher")]
public class Ability_RocketLauncher : Ability
{
    [HideInInspector] public Vector3 m_destination;

    [Header("Rocket Specific")]
    public float m_radius;


    public override void TriggerAbility()
    {
        m_launcher.Launch();    
    }

    public override void Initilise(Rigidbody projectileObj, Transform playerGunPos)
    {
        var dest = GetAbilityPointInWorldSpace();
        SetRocketDestination(dest);
        var velocity = BallisticVelocity(m_destination, 45f, playerGunPos.position);

        m_launcher = projectileObj.GetComponent<Projectile>();
        m_launcher.m_damage = m_damage;
        m_launcher.m_velocity = velocity;
        m_launcher.m_prefab = m_bulletPrefab;
        m_launcher.m_spawnPos = playerGunPos;
        m_launcher.m_radius = m_radius;
        m_launcher.m_explosionFX = m_explosionFX;
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
