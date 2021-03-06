﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{

    public int m_rewardAmount = 10;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Player playerController = collision.collider.GetComponent<Player>();

            playerController.m_score += m_rewardAmount;
            Destroy(gameObject);
        }
        
    }

}
