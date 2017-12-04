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
    public List<Player> m_playerControllers = new List<Player>();
    public int m_bushID;        // TODO: This will need to be incremented for every bush on the map


    // TODO: public Dictionary<int, List<PlayerController>>
    // Then always reveal all the players under the id of the bush

    private void FixedUpdate()
    {
        CheckForNearbyPlayers();
    }

    void CalculateArea()
    {
        // Center POsition
        // Calculate area from center position

        // If players enter this any position in this area, then hide them.
    }

    

    //private void OnTriggerEnter(Collider other)
    //{
    //    var player = other.GetComponent<Player>();

    //    if (other.GetComponent<Player>())
    //        player.RpcHidePlayer(true);
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    var player = other.GetComponent<Player>();

    //    if (other.GetComponent<Player>())
    //        player.RpcHidePlayer(false);
    //}


    void CheckForNearbyPlayers()
    {
        for (int i = 0; i < m_playerControllers.Count; i++)
        {
            var playerTransform = m_playerControllers[i].GetComponent<Transform>();
            float deltaDistance = Vector3.Distance(transform.position, playerTransform.position);
            MeshRenderer r = m_playerControllers[i].GetComponent<MeshRenderer>();

            if (deltaDistance >= m_hidingDistance)
            {
                //m_playerControllers[i].m_hidingInBush.Add(m_bushID, true);
                m_playerControllers[i].RpcHidePlayer(true);
            }
            else
            {
                //m_playerControllers[i].m_hidingInBush.Remove(m_bushID);
                m_playerControllers[i].RpcHidePlayer(false);

            }
        }
    }
}
