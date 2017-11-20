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
    public List<Text> m_uiPlayerScores;

    private Canvas m_canvas;


    private void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_canvas.enabled = false;
    }

    public void UpdateScoreboard()
    {
        string[] playerNames = GameManager.Instance.getPlayerNames();
        int[] playerScores = GameManager.Instance.getPlayerScores();

        GameManager.Instance.RpcUpdateScoreboard(playerNames, playerScores);
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
