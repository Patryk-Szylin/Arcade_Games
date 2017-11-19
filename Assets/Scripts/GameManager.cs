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

    [SyncVar] public int m_playerCount = 0;
    public static List<Player> m_allPlayers = new List<Player>();
    public List<Text> m_labelPlayerNames = new List<Text>();
    public List<Text> m_labelPlayerScores = new List<Text>();

    [Header("Others")]
    public Text m_waitingMsgText;
    private string m_waitingString = "Waiting for Players";

    [SyncVar] bool m_gameOver = false;
    Player m_winner;

    [Server]
    private void Start()
    {
        StartCoroutine("GameLoop");
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
        yield return new WaitForSeconds(2f);
    }

    [ClientRpc]
    private void RpcStartGame()
    {
        UpdateMessage("Game has begun!");

        EnablePlayers(false);
    }

    Player GetWinner()
    {
        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            if (m_allPlayers[i].m_score >= m_maxScore)
                return m_allPlayers[i];
        }

        return null;
    }

    public void CheckScores()
    {
        m_winner = GetWinner();

        if (m_winner != null)
            m_gameOver = true;
    }


    IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);
        RpcPlayGame();

        while (m_gameOver == false)
        {
            CheckScores();
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
        RpcEndGame();
        RpcUpdateMessage("Game Over \n " + m_winner.m_pSetup.m_playerName + " is the winner!"); // Later add a text that announces a winner      
        yield return new WaitForSeconds(5f);
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
        }
    }


    // SCOREBOARD UI functionality
    [ClientRpc]
    public void RpcUpdateScoreboard(string[] playerNames, int[] playerScores)
    {
        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            if (playerNames[i] != null)
            {
                UI_Scoreboard.Instance.m_uiPlayerNames[i].text = playerNames[i];
            }

            if (playerScores[i] != null)
            {
                UI_Scoreboard.Instance.m_uiPlayerScores[i].text = playerScores[i].ToString();
            }
        }
    }

    //[Server]
    //public void UpdateScoreboard()
    //{
    //    string[] names = new string[m_allPlayers.Count];
    //    int[] scores = new int[m_allPlayers.Count]; ;

    //    for (int i = 0; i < m_allPlayers.Count; i++)
    //    {
    //        if(m_allPlayers[i] != null)
    //        {
    //            names[i] = m_allPlayers[i].GetComponent<PlayerSetup>().m_playerName;
    //            scores[i] = m_allPlayers[i].m_score;
    //        }
    //    }

    //    RpcUpdateScoreboard(names, scores);
    //}

    //public Dictionary<string, int> getPlayerNamesAndScores()
    //{
    //    Dictionary<string, int> temp = new Dictionary<string, int>();
    //    var names = new string[m_allPlayers.Count];
    //    var scores = new int[m_allPlayers.Count];

    //    for (int i = 0; i < m_allPlayers.Count; i++)
    //    {
    //        var name = m_allPlayers[i].GetComponent<PlayerSetup>().m_playerName;
    //        var score = scores[i] = m_allPlayers[i].m_score;

    //        temp.Add(name, score);            
    //    }

    //    return temp;
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

    public int[] getPlayerScores()
    {
        var scores = new int[m_allPlayers.Count];

        for (int i = 0; i < m_allPlayers.Count; i++)
        {
            scores[i] = m_allPlayers[i].m_score;
        }

        return scores;
    }




}
