using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour
{
    // DRAW COLIN HERE DANCING 
    public Ability m_abilityDrop;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCast>();
        //var maxAbilityCount = player.GetMaxAbilityCount();
        //var lastAbility = maxAbilityCount - 1;

        if (player != null)
        {
            print("TRIGGERED");
            player.EquipNewAbility(m_abilityDrop);
            player.m_abilities[4] = m_abilityDrop;
            player.m_abilitiesReady[4] = true;
            player.m_nextAbilityReadyTime[4] = 0;
            player.m_cooldownLeft[4] = 0;
            player.m_abilitySprites[4] = m_abilityDrop.m_abilityIcon;
            Destroy(this.gameObject);
        }
    }


}
