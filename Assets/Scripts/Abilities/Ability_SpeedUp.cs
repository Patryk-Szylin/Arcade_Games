using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Speed Boost")]
public class Ability_SpeedUp : Ability
{
    [Header("Speed Up Specific")]
    public float m_speedMultiplier;
    public float m_duration;

    [HideInInspector] public PlayerMovement m_player;

    public override string getToolTipStatInfo()
    {
        return string.Format("<size= 16> {0} </size>", m_name);
    }

    public override void Initilise(Rigidbody targetObj, Transform PlayerGunPos, Vector3 destination)
    {
        m_player = targetObj.GetComponent<PlayerMovement>();
    }

    public override void TriggerAbility()
    {
        //m_player.GetComponent<PlayerMovement>().m_currentMoveSpeed = 15f;
        m_player.m_Boosted = 1.4f;
    }
}
