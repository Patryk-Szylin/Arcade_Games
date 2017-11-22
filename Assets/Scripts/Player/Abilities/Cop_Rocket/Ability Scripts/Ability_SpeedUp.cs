using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed Boost")]
public class Ability_SpeedUp : AbilityPassive
{
    [Header("Speed Up Specific")]
    public float m_speedMultiplier;
    public float m_duration;

    [HideInInspector] public PlayerMovement m_player;

    public override void Initilise(Transform playerGunPos, string sourceID, Vector3 dir)
    {
        //m_player = gameObject.GetComponent<PlayerMovement>();
    }

    public override void TriggerAbility()
    {
        //m_player.GetComponent<PlayerMovement>().m_currentMoveSpeed = 15f;
        //m_player.m_Boosted = 1.4f;
    }
}
