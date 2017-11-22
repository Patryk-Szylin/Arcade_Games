using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    float cursorSizeX = 32;  // Your cursor size x
    float cursorSizeY = 32;  // Your cursor size y
    // Use this for initialization
    void Start () {
        Cursor.visible = false;
    }

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
        {
            cursorSizeX = Mathf.Lerp(16, 32, Time.deltaTime);
            cursorSizeY = Mathf.Lerp(16, 32, Time.deltaTime);
        }
        else
        {
            cursorSizeX = Mathf.Lerp(32, 16, Time.deltaTime);
            cursorSizeY = Mathf.Lerp(32, 16, Time.deltaTime);
        }
	}
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorSizeX, cursorSizeY), cursorTexture);
    }
}
