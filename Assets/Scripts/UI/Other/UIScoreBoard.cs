using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreBoard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboardItem;

    [SerializeField]
    Transform scoreboardList;

    void OnEnable()
    {
        Player[] players = GameManager.GetAllPlayers();

        foreach (Player player in players)
        {
            PlayerSetup playerName = player.GetComponent<PlayerSetup>();
            GameObject panelGO = Instantiate(playerScoreboardItem, scoreboardList) as GameObject;
            PlayerScoreboardPanel panel = panelGO.GetComponent<PlayerScoreboardPanel>();
            if (panel != null)
            {
                panel.Setup(playerName.m_playerName, player.kills, player.deaths);
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

