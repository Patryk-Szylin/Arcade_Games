using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
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

        if (player != null)
        {
            print("TRIGGERED");
            player.m_abilities[3] = m_abilityDrop;
            player.m_abilitiesReady[3] = true;
            player.m_nextAbilityReadyTime[3] = 0;
            player.m_cooldownLeft[3] = 0;
            player.m_abilitySprites[3] = m_abilityDrop.m_abilityIcon;
            Destroy(this.gameObject);
        }
    }


}
