using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Swap : MonoBehaviour {
    public GameObject ToSwap;
    public GameObject Current;
    // Use this for initialization
    void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        ToSwap.SetActive(true);
        Current.SetActive(false);

    }
}
