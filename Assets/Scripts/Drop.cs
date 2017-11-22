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
        AbilitiesManager player = other.GetComponent<AbilitiesManager>();        

        if (player != null)
        {
            player.abilities[4] = m_abilityDrop;
            player.updateUI(4);
            Destroy(this.gameObject);
        }
    }


}
