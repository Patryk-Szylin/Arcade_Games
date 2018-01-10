using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {
    public bool first_open = true;
    public GameObject Main;
	// Update is called once per frame
	void Update () {
		if(this.enabled && first_open)
        {
            Main.SetActive(true);
        }

        first_open = false;
	}
}
