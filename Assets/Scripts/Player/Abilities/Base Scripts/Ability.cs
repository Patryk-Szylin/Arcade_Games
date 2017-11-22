using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public abstract class Ability : ScriptableObject
{
    [Header("Ability Settings")]
    public float cooldown;
    public bool quickCast;
    [Range(0, 100)]
    public float movementSlow;
    public float movementSlowDuration;
    public float chargeTimer;

    [Header("Variables")]
    public Sprite uiSprite;
    public string abilityTitle;
    public string toolTip;
    public string abilityStatInfo;
    public Sprite indicator;
    public AudioClip soundEffect;

    // @ Abstract functions
    public abstract void TriggerAbility();
    public abstract void Initilise(Transform projectileSpawnLocation, string souceID, Vector3 direciton);


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
}
