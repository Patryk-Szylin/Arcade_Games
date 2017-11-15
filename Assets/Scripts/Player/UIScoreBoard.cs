using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreBoard : MonoBehaviour
{
    [SerializeField]
    Team_Manager team;

    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform scoreboardList;

    void OnEnable()
    {
        GameObject localplayer = team.localPlayer;
        GameObject[] players = team.enemyTeam;

        if (localplayer == null)
            return;

        // Local Player Panel
        PlayerSetup playerName = localplayer.GetComponent<PlayerSetup>();
        Player playerInfo = localplayer.GetComponent<Player>();
        GameObject panelGO = Instantiate(playerScoreboardItem, scoreboardList) as GameObject;
        PlayerScoreboardPanel panel = panelGO.GetComponent<PlayerScoreboardPanel>();
        if (panel != null)
        {
            panel.Setup(playerName.m_playerName, playerInfo.kills, playerInfo.deaths);
        }

        // Enemies
        foreach (GameObject player in players)
        {
            playerName = localplayer.GetComponent<PlayerSetup>();
            playerInfo = localplayer.GetComponent<Player>();
            panelGO = Instantiate(playerScoreboardItem, scoreboardList) as GameObject;
            panel = panelGO.GetComponent<PlayerScoreboardPanel>();
            if (panel != null)
            {
                panel.Setup(playerName.m_playerName, playerInfo.kills, playerInfo.deaths);
            }
        }
    }

    void OnDisable ()
    {
        foreach(Transform child in scoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}

