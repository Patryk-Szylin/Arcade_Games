using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Abilities/Speed Up")]
public class Ability_SpeedUp : Ability
{
    public override void Initilise(Rigidbody projectileObj, Transform playerGunPos)
    {
        projectileObj = null;
        playerGunPos = null;
    }

    public override void TriggerAbility()
    {
        

    }
}
