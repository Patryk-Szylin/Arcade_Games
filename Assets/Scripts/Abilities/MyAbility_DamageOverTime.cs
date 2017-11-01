using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoT", menuName = "Abilities/DoT ", order = 2)]
public class MyAbility_DamageOverTime : MyAbility
{
    [Header("Dot Specific")]
    public float m_timeInterval;
    public int m_maximumTicks;
    public float m_damagePerTick;

    public override void ApplyTo(PlayerController pc)
    {
        Utility utility = FindObjectOfType<Utility>();
        utility.ApplyDotEffect(m_maximumTicks, m_timeInterval, () => { pc.GetComponent<PlayerHealth>().m_currentHealth -= m_damagePerTick; });     
    }


    public override void ApplyToPlayer()
    {
        throw new System.NotImplementedException();
    }

}
