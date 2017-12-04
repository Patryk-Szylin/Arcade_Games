using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadLevel : MonoBehaviour {

    public Object LevelToLoad;
    public Canvas LobbyManager;
    public bool Exit;
    private void Start()
    {
        Button Btn = this.GetComponent<Button>();
        Btn.onClick.AddListener(OnClick);
    }
    void OnClick()
    {
        if (!Exit)
        {
            string LevelName = LevelToLoad.name;
            SceneManager.LoadScene(LevelName);
            if (LobbyManager != null)
            {
                LobbyManager.enabled = false;
            }
        }
        else
        {
            Application.Quit();
        }
    }
}
