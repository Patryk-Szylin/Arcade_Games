using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedPanel : MonoBehaviour {

    [SerializeField]
    Text text;

    //start - co

    // Use this for initialization
    public void Setup(string player, string source) {
        text.text = "<b><color=magenta>" + source + "</color></b> killed <b>" + player + "</b>";
    }

    //co fade
}
