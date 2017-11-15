﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour {

    [SerializeField]
    GameObject killfeedPanelPrefab;

	// Use this for initialization
	void Start () {
        GameManager.Instance.onPlayerKilledCallback += OnKill;
	}
	
    public void OnKill (string player, string source)
    {
        GameObject go = Instantiate(killfeedPanelPrefab, this.transform);
        //go.SetSiblingIndex(0);
        go.GetComponent<KillFeedPanel>().Setup(player, source);

        Destroy(go, 4f);
    }
}
