using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Ability: ScriptableObject
{
    [Header("Ability Settings")]
    [HideInInspector] public Vector3 m_direction;
    public Rigidbody m_bulletPrefab;
    public float m_force;

    [Header("Ability Effects")]
    public GameObject m_abilityFX;      // For shooting/firing effect
    public GameObject m_impactFX;       // e.g. Explosion
    public GameObject m_trailFX = null; // Used if there's a trail 

    // @ Abstract functions
    public abstract void TriggerAbility();
    public abstract void Initilise(Rigidbody projectileObj, Transform PlayerGunPos);


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
