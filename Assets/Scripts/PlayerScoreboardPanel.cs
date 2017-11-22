using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardPanel : MonoBehaviour {

    [SerializeField]
    Text playernameText;
    [SerializeField]
    Text kdaText;

    public void Setup(string playername, int kills, int deaths)
    {
        playernameText.text = playername;
        kdaText.text = "" + kills + " / " + deaths;
    }
}
