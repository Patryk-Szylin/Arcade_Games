using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Gold : NetworkBehaviour
{

    public int m_rewardAmount = 10;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();

            playerController.m_score += m_rewardAmount;
            Destroy(gameObject);
        }
        
    }

}
