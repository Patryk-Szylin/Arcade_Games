using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpeedBoost", menuName = "Abilities/Speed Boost", order = 3)]
public class MyAbility_MoveSpeedBoost : MyAbility
{
    [Header("Speed Boost Specific")]
    public float m_speedMultiplier;
    public int m_speedDuration;

    public override void ApplyTo(PlayerController pc)
    {
        Debug.Log("applying speed boost");
        Utility utility = FindObjectOfType<Utility>();
        var movement = pc.GetComponent<PlayerMovement>();
        var regularSpeed = movement.m_moveSpeed;

        if (!movement.m_isBoosted)
        {
            utility.ApplySpeedEffect(() => { movement.m_moveSpeed = movement.m_moveSpeed * m_speedMultiplier;
                                             movement.m_isBoosted = true; },
                                        m_speedDuration,
                                     () => { movement.m_moveSpeed = regularSpeed;
                                             movement.m_isBoosted = false;});            
        }

    }

    public override void ApplyToPlayer()
    {
        throw new System.NotImplementedException();
    }
}
