using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby; // Allows me to use Network lobby components

public class GameManager : NetworkBehaviour
{


    #region Singleton


    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
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

    [Header("Game Options")]
    public int m_maxScore = 2;
    [SyncVar] public float m_timeLimitInSeconds = 15f;

    public static List<Player> m_allPlayers = new List<Player>();
    public List<Text> m_labelPlayerNames = new List<Text>();
    public List<Text> m_labelPlayerKills = new List<Text>();
    public List<Text> m_labelPlayerDeaths = new List<Text>();

    [Header("Others")]
    public Text m_waitingMsgText;
    public Text m_gameTimerText;
    private string m_waitingString = "Waiting for Players";

    [SyncVar] public bool m_gameOver = false;
    private bool m_hasGameStarted = false;
    Player m_winner;

    [Server]
    private void Start()
    {
        RpcSetTimerText();
        StartCoroutine("GameLoop");
    }

    [ClientRpc]
    void RpcSetTimerText()
    {
        m_gameTimerText.text = m_timeLimitInSeconds.ToString();
    }

    private void Update()
    {
        if (m_hasGameStarted)
        {
            RpcStartTimer();
        }
    }

    [ClientRpc]
    void RpcStartTimer()
    {
        m_timeLimitInSeconds -= Time.deltaTime;
        var timer = Mathf.RoundToInt(m_timeLimitInSeconds);
        if (timer <= 0)
        {
            timer = 0;
            m_gameOver = true;
        }

        m_gameTimerText.text = timer.ToString();
    }

    IEnumerator GameLoop()
    {
        LobbyManager lobbyManager = LobbyManager.s_Singleton;

        if (lobbyManager != null)
        {
            while (m_allPlayers.Count < lobbyManager._playerNumber)
            {
                yield return null; // Wait one frame and wait till all players connect to the server
            }

            EnablePlayers(false);
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine("StartGame");
            yield return StartCoroutine("Play");
            yield return StartCoroutine("GameOver");
            StartCoroutine("GameLoop");
        }
        else
        {
            Debug.LogWarning("________Launch game from lobby scene only!________");
        }
    }

    IEnumerator StartGame()
    {
        Reset();
        RpcStartGame();
        UI_Scoreboard.Instance.UpdateScoreboard();
        //UpdateScoreboard();
        yield return new WaitForSeconds(2f);
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        UpdateMessage("Game has begun!");



        EnablePlayers(false);
    }

    // Later this needs to check if other players have the same kill amount, then
    // Take into account accuracy.
    Player GetWinner()
    {
        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            for (int j = 0; j < m_allPlayers.Count; j++)
            {
                var highestPlayer = m_allPlayers[i];

                if (highestPlayer.m_kills < m_allPlayers[j].m_kills)
                {
                    highestPlayer = m_allPlayers[j];
                    return highestPlayer;
                }
            }
        }

        return null;
    }

    public void CheckScores()
    {
        m_winner = GetWinner();
        m_gameOver = true;

        //if (m_winner != null)
        //    m_gameOver = true;
    }


    IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);
        m_hasGameStarted = true;
        RpcPlayGame();

        while (m_gameOver == false)
        {
            yield return null;
        }

        yield return null;
    }

    [ClientRpc]
    private void RpcPlayGame()
    {
        EnablePlayers(true);
        UpdateMessage("");
    }

    IEnumerator GameOver()
    {
        print("Game over");
        m_hasGameStarted = false;
        CheckScores();
        RpcEndGame();
        RpcUpdateMessage("Game Over \n " + m_winner.m_pSetup.m_playerName + " is the winner!"); // Later add a text that announces a winner      
        //RpcUpdateMessage("GAME OVER !!!!!");            // TODO: When KDA is merged with this, display the winner's name as well

        yield return new WaitForSeconds(10f);
        Reset();

        LobbyManager.s_Singleton._playerNumber = 0;
        LobbyManager.s_Singleton.SendReturnToLobby();
    }

    [ClientRpc]
    private void RpcEndGame()
    {
        EnablePlayers(false);
    }

    void EnablePlayers(bool state)
    {
        Player[] players = FindObjectsOfType<Player>();

        foreach (Player p in players)
        {
            p.enabled = state;
        }
    }


    [ClientRpc]
    void RpcUpdateMessage(string msg)
    {
        UpdateMessage(msg);
    }

    public void UpdateMessage(string msg)
    {
        m_waitingMsgText.gameObject.SetActive(true);
        m_waitingMsgText.text = msg;
    }

    private void Reset()
    {
        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            var playerHealth = m_allPlayers[i].GetComponent<PlayerHealth>();
            playerHealth.Reset();
            m_allPlayers[i].m_score = 0;
            m_allPlayers[i].m_kills = 0;
            m_allPlayers[i].m_deaths = 0;

        }
    }

    [Server]
    public void UpdateScoreboard()
    {
        string[] playerNames = new string[m_allPlayers.Count];
        int[] playerKills = new int[m_allPlayers.Count];
        int[] playerDeaths = new int[m_allPlayers.Count];

        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            if(m_allPlayers[i] != null)
            {
                //playerNames[i] = m_allPlayers[i].m_pSetup.m_playerName;
                playerNames[i] = m_allPlayers[i].GetComponent<PlayerSetup>().m_playerName;
                playerKills[i] = m_allPlayers[i].m_kills;
                playerDeaths[i] = m_allPlayers[i].m_deaths;
            }
        }

        RpcUpdateScoreboard(playerNames, playerKills, playerDeaths);
    }

    [ClientRpc]
    public void RpcUpdateScoreboard(string[] playerNames, int[] playerKills, int[] playerDeaths)
    {
        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            if (playerNames[i] != null)
            {
                m_labelPlayerNames[i].text = playerNames[i];
            }

            if (playerKills[i] != null)
            {
                m_labelPlayerKills[i].text = playerKills[i].ToString();
            }

            if (playerDeaths[i] != null)
            {
                m_labelPlayerDeaths[i].text = playerDeaths[i].ToString();
            }
        }

    }



    //// SCOREBOARD UI functionality
    //[ClientRpc]
    //public void RpcUpdateScoreboard(string[] playerNames, int[] playerKills, int[] playerDeaths)
    //{
    //    print(m_allPlayers.Count);
    //    for (int i = 0; i < m_allPlayers.Count; i++)
    //    {
    //        if (playerNames[i] != null)
    //        {
    //            UI_Scoreboard.Instance.m_uiPlayerNames[i].text = playerNames[i];
    //        }

    //        UI_Scoreboard.Instance.m_uiPlayerKills[i].text = playerKills[i].ToString();
    //        UI_Scoreboard.Instance.m_uiPlayerDeaths[i].text = playerDeaths[i].ToString();
    //    }
    //}

    public string[] getPlayerNames()
    {
        var names = new string[m_allPlayers.Count];

        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            names[i] = m_allPlayers[i].GetComponent<PlayerSetup>().m_playerName;
        }

        return names;
    }

    public int[] getPlayerKills()
    {
        var kills = new int[m_allPlayers.Count];

        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            kills[i] = m_allPlayers[i].m_kills;
        }

        return kills;
    }

    public int[] getPlayerDeaths()
    {
        var deaths = new int[m_allPlayers.Count];

        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            deaths[i] = m_allPlayers[i].m_deaths;
        }

        return deaths;
    }






}
