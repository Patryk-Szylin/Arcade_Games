using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{


    #region Singleton

    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
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
    public int m_minimumPlayers = 2;
    [SyncVar]
    public int m_playerCount = 0;
    public Color[] m_playerColours = { Color.red, Color.green, Color.blue, Color.yellow };

    [Header("Others")]
    public Text m_waitingMsgText;
    private string m_waitingString = "Waiting for Players";

    private void Start()
    {
        StartCoroutine("GameLoop");
    }

    IEnumerator GameLoop()
    {
        yield return StartCoroutine("Lobby");
        yield return StartCoroutine("Play");
        yield return StartCoroutine("GameOver");
    }

    IEnumerator Lobby()
    {
        m_waitingMsgText.text = m_waitingString;
        m_waitingMsgText.gameObject.SetActive(true);

        while(m_playerCount < m_minimumPlayers)
        {
            EnablePlayers(false);
            yield return null;
        }        
    }

    IEnumerator Play()
    {
        m_waitingMsgText.gameObject.SetActive(false);

        EnablePlayers(true);

        yield return null;
    }

    IEnumerator GameOver()
    {
        yield return null;
    }

    void EnablePlayers(bool state)
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach(PlayerController p in players)
        {
            p.enabled = state;
        }
    }

    public void AddPlayer(PlayerSetup pSetup)
    {
        pSetup.m_playerColor = m_playerColours[m_playerCount];
        pSetup.m_id = m_playerCount + 1;

        // Also add player to the bush
        var bushes = FindObjectsOfType<Bush>();
         
        foreach (Bush b in bushes)
        {
            var playerController = pSetup.GetComponent<PlayerController>();
            b.m_playerControllers.Add(playerController);
        }
    }





}
