using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Load : MonoBehaviour {

    public Canvas ToSwap;
    public Canvas Current;
    public Canvas MainMenuM;
    public bool SwappingToLobby;
    public bool SwappingBack;
    public Camera C1;
    public Camera C2;

    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        ToSwap.enabled = true;
        Current.enabled = false;
        if (SwappingToLobby)
        {
            MainMenuM.enabled = false;
            C2.enabled = true;
            C1.enabled = false;

        }
        if (SwappingBack)
        {
            MainMenuM.enabled = true;
            C1.enabled = true;
            C2.enabled = false;
        }

    }
}
