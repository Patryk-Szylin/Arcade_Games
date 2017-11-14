using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


/// <summary>
/// 
/// @description    :                           This is a base class for creating new abiltiies, every deriving class will have to implement 
///                                                 TriggerAbility();
///                                                 Initilise(...);
///                   
///                                             Scriptable object in this case behaves as a data storage and lives in unity as an .asset, therefore designers have easier approach creating
///                                             variations of particular effects (e.g. Different rockets, with different effects and stats).
///                   
///                                             An example use case of this class is a Ability_RocketLauncher script. It implements Initlise() by using 
///                                             the values from base and its own class (Ability.cs and Ability_RocketLauncher.cs) and setting those values in its target projectile that
///                                             it is going to use. i.e. Projectile_Rocket.cs.
///                   
///                                                                     Initilise and Trigger Ability are called within <para name="PlayerCast.cs">
///                   
///                     
/// <para name="Initlise(...)">                 Initilise is used purely for setting projectile's values before instantiation </para>
/// <para name="Initlise(RigidBody targetObj)"> This paramater is used as a reference to projectile object. Each projectile will contain different behaviours
///                                             for different abilities. 
///                                             
///                                             Every derived class will use m_projectilePrefab by getting appropriate component from targetObj.
///                                             Rocket Use Case: m_projectilePrefab gets Projectile_Rocket.cs component and sets its values. 
///                                             
/// <para name="TriggerAbility()">              It calls m_projectilePrefab's <para name="Launch()"> Function which instantiates projectiles.
/// 
///                                             
/// <para name="m_projectilePrefab">            A reference to projectile prefab. Each ability type will have different projectiles. Each projectile implments Launch() and OnCollisionHit() 
///                                             Upon initialisation the projectile's values will be overwritten by Ability-Specific values.  
/// 
/// <para name="GetAbilityPointInWorldSpace()"> Returns mouse position. 
/// </summary>

public abstract class Ability: ScriptableObject
{
    [Header("Ability Settings")]
    [HideInInspector] public Vector3 m_direction;
    public Rigidbody m_projectilePrefab;
    public float m_force;
    public int m_cooldown;              // TODO

    [Header("UI Related")]
    public string m_name;
    public Sprite m_abilityIcon;
    public string m_description;



    [Header("Ability Effects")]
    public GameObject m_abilityFX;      // For shooting/firing effect
    public GameObject m_impactFX;       // e.g. Explosion
    public GameObject m_trailFX = null; // Used if there's a trail 

    // @ Abstract functions
    public abstract void TriggerAbility();
    public abstract void Initilise(Rigidbody targetObj, Transform PlayerGunPos);
    public abstract String getToolTipStatInfo();


    // This should be in utility class
    public Vector3 GetAbilityPointInWorldSpace()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var rayWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 99999f))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    public Player getProjectileOwner(Transform fromGunPos)
    {
        return fromGunPos.GetComponentInParent<Player>();
    }

    
}


