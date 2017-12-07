using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class makeMapObject : NetworkBehaviour {

    public Image image;

	// Use this for initialization
	void Start ()
    {
        MiniMapController.RegisterMapObject(this.gameObject, image);
	}

    //public override void OnStartLocalPlayer()
    //{
    //    MiniMapController.RegisterMapObject(this.gameObject, image);
    //}

    // Update is called once per frame
    void Update ()
    {
		
	}

    private void OnDestroy()
    {
        MiniMapController.RemoveMapObject(this.gameObject);
    }
}
