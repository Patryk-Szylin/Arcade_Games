using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Drop : NetworkBehaviour
{

    // DRAW COLIN HERE DANCING  
    public Ability m_abilityDrop;

    public List<Ability> m_abilitiesSelection;


    private void Start()
    {
        var randomIndex = Random.Range(0, m_abilitiesSelection.Count);
        var randomAbility = m_abilitiesSelection[randomIndex];

        m_abilityDrop = randomAbility;
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCast>();
        //var maxAbilityCount = player.GetMaxAbilityCount();
        //var lastAbility = maxAbilityCount - 1;

        if (player != null)
        {
            print("TRIGGERED");
            player.m_abilities[4] = m_abilityDrop;
            player.m_abilities[4].m_abilityIcon = m_abilityDrop.m_abilityIcon;
            //player.m_abilitiesReady[4] = true;
            //player.m_nextAbilityReadyTime[4] = 0;
            //player.m_cooldownLeft[4] = 0;
            //player.m_abilitySprites[4] = m_abilityDrop.m_abilityIcon;
            Destroy(this.gameObject);
        }
    }


}
