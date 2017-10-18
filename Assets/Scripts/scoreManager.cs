using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class scoreManager : NetworkBehaviour
{

    public Text text;

    public static int overallLoot = 0;


    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        overallLoot = 0;
    }

    private void Update()
    {
        text.text = "Loot: " + overallLoot;
    }
}
