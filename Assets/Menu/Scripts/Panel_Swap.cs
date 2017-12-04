using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Swap : MonoBehaviour {
    public Canvas ToSwap;
    public Canvas Current;
    // Use this for initialization
    void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        ToSwap.enabled = true;
        Current.enabled = false;

    }
}
