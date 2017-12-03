using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Scoreboard : MonoBehaviour
{

    #region SINGLETON
    private static UI_Scoreboard _instance;
    public static UI_Scoreboard Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UI_Scoreboard>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("UI_Scoreboard").AddComponent<UI_Scoreboard>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public List<Text> m_uiPlayerNames;
    public List<Text> m_uiPlayerKills;
    public List<Text> m_uiPlayerDeaths;


    private Canvas m_canvas;


    private void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    public void UpdateScoreboard()
    {
        var players = GameManager.m_allPlayers;

        //string[] playerNames = GameManager.Instance.getPlayerNames();
        //int[] playerKills = GameManager.Instance.getPlayerKills();
        //int[] playerDeaths = GameManager.Instance.getPlayerDeaths();

        string[] playerNames = new string[players.Count];
        int[] playerKills = new int[players.Count];
        int[] playerDeaths = new int[players.Count];

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                //playerNames[i] = m_allPlayers[i].m_pSetup.m_playerName;
                playerNames[i] = players[i].GetComponent<PlayerSetup>().m_playerName;


                playerKills[i] = players[i].m_kills;
                playerDeaths[i] = players[i].m_deaths;
            }
        }

        GameManager.Instance.RpcUpdateScoreboard(playerNames, playerKills, playerDeaths);
    }


    public void ShowScoreboard()
    {
        m_canvas.enabled = true;
    }

    public void HideScoreboard()
    {
        m_canvas.enabled = false;
    }

}
