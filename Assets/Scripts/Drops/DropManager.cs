using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class DropManager : NetworkBehaviour
{  
    public List<Ability> m_abilitiesSelection;

    public List<GameObject> m_spawnPoints = new List<GameObject>();
    public GameObject m_dropPrefab;


    [Server]
    private void Start()
    {
        //var randomIndex = Random.Range(0, m_abilitiesSelection.Count);
        //var randomAbility = m_abilitiesSelection[randomIndex];

        //m_abilityDrop = randomAbility;
        RpcGenerateRandomAbilities();
    }

    [ClientRpc]
    public void RpcGenerateRandomAbilities()
    {
        var randomIndex = Random.Range(0, m_abilitiesSelection.Count);
        var randomAbility = m_abilitiesSelection[randomIndex];

        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            var spawnPos = m_spawnPoints[i].transform;

            var drop = Instantiate(m_dropPrefab, spawnPos.position, spawnPos.rotation) as GameObject;
            drop.GetComponent<Drop>().m_abilityDrop = randomAbility;
            NetworkServer.Spawn(drop);
            //m_dropPrefab.GetComponent<Drop>().m_abilityDrop = randomAbility;
        }

        
    }


}
