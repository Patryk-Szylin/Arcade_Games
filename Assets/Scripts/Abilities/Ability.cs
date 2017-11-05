using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [HideInInspector] public Projectile m_launcher;
    public Rigidbody m_bulletPrefab;
    public float m_damage;


    // Effects
    public GameObject m_explosionFX;
    public GameObject m_trailFX;
    //public AudioSource m_effectSound;
    //public AudioSource m_ExplosionSound;


    public abstract void TriggerAbility();
    public abstract void Initilise(Rigidbody projectileObj, Transform playerGunPos);



    // This should be in utility class
    public Vector3 GetAbilityPointInWorldSpace()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 99999f))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
