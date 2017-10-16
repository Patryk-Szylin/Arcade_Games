using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// This class is responsible for detecting nearby players, and hiding them whenever they enter the hiding distace. 
/// 
/// @ Author:                  Patryk Szylin
/// @ Date:                    16/10/2017
/// @ Collaborated Scripts:    GameManager.cs PlayerController.cs
/// @ m_playerControllers this variable gets updated whenever a Client connects to the server. (Check GameManager's AddPlayer()) 
/// 
/// </summary>


public class Bush : NetworkBehaviour
{

    [Header("Bush Options")]
    public float m_hidingDistance = 5f; // TODO: This will need to be adjusted so when there are different sizes of the bush, it automatically gets resized.

    [Header("Debug Stats")]
    public List<PlayerController> m_playerControllers = new List<PlayerController>();

    private void FixedUpdate()
    {
        CheckForNearbyPlayers();
    }

    void CheckForNearbyPlayers()
    {
        for (int i = 0; i < m_playerControllers.Count; i++)
        {
            var playerTransform = m_playerControllers[i].GetComponent<Transform>();

            float deltaDistance = Vector3.Distance(transform.position, playerTransform.position);

            MeshRenderer r = m_playerControllers[i].GetComponent<MeshRenderer>();

            if (deltaDistance >= m_hidingDistance)
            {
                m_playerControllers[i].RpcHidePlayer(true);
            }
            else
            {
                m_playerControllers[i].RpcHidePlayer(false);

            }
        }
    }
}
