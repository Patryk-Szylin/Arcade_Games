using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// </summary>


public abstract class Ability : NetworkBehaviour
{
    // Asthetics
    public ParticleSystem m_castEffect;
    public ParticleSystem m_travelEffect;
    public AudioSource m_soundEffect;

    public float m_range;
    public float m_cooldown;

    public Vector3 AbilityDir { get; private set; }
    public Vector3 AbilityPoint { get; private set; }

    // @ VIRTUALS
    public virtual void OnAbilityHit() { }
    public virtual void OnAbilityCast() {
        AbilityDir = GetAbilityDirection(m_range);
        AbilityPoint = GetAbilityPointInWorldSpace(m_range);
    }

    //public virtual void OnAbilityCast()

    public Vector3 GetAbilityDirection(float range)
    {
        Vector3 mouseLoc;
        Vector3 dir = Vector3.zero;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            mouseLoc = hit.point;
            dir = mouseLoc - transform.position;
            Debug.DrawLine(transform.position, mouseLoc);
        }
        return dir;
    }

    public Vector3 GetAbilityPointInWorldSpace(float range)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            return hit.point;
        }

        return Vector3.zero;     
    }

    public PlayerController GetPlayerOnHit(float range)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                var pc = hit.collider.GetComponent<PlayerController>();
                return pc;               
            }
        }

        return null;
    }

}
