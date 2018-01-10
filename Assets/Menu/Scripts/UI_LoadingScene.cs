using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingScene : MonoBehaviour {

    public Material rend;
    public Text Progress;
    float change;
    public float addValue;
    public bool Loaded;

    //Load Next Scene
    public Canvas ToSwap;
    public Canvas Current;
    public Canvas MainMenuM;
    public bool SwappingToLobby;
    public bool SwappingBack;
    public Camera C1;
    public Camera C2;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (this.GetComponent<Canvas>().enabled)
        {
            if (change < 1)
            {
                if (change < 1)
                {
                    change = change + addValue;
                    Progress.text = Mathf.Round(change * 100) + "%";
                }
                else
                {
                    change = 1;
                    Progress.text = Mathf.Round(change * 100) + "%";
                }
                rend.SetFloat("_Cutoff", change);
            }
            else
            {
                Loaded = true;
            }
            if (Loaded == true)
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
	}
}
