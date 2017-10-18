using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DropOff : NetworkBehaviour
{

    
    private void OnTriggerEnter(Collider collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();

        scoreManager.overallLoot += playerController.m_score;

        playerController.m_score = 0;
    }




}
