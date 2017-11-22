using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Taser", fileName = "Taser")]
public class Ability_Taser : AbilityRayCast {

    private RaycastShootTriggerable rcShoot;

    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Color laserColor = Color.white;


    public override void Initilise(Transform projectileSpawnLocation, string sourceID, Vector3 dir)
    {
        //rcShoot = GetComponent<RaycastShootTriggerable>();
        //rcShoot.Initialize();

        //rcShoot.gunDamage = gunDamage;
        //rcShoot.weaponRange = weaponRange;
        //rcShoot.hitForce = hitForce;
        //rcShoot.laserLine.material = new Material(Shader.Find("Unlit/Color"));
        //rcShoot.laserLine.material.color = laserColor;
    }

    public override void TriggerAbility()
    {
        //rcShoot.Fire();
    }
}
