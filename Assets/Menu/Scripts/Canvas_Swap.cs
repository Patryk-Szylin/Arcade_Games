using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Swap : MonoBehaviour {
    //Set Canvas
    public Canvas ToSwap;
    public Canvas Current;
    public bool BackMainMenu;
    // Use this for initialization
    void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
    public void TaskOnClick()
    {
        ToSwap.enabled = true;
        Current.enabled = false;
        if (BackMainMenu)
        {
            Current.GetComponent<Settings>().first_open = true;
        }
    }
}
