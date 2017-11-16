using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    [Header("Camera Restriction")]
    public float xPosRestriction = 10;
    public float yPosRestriction = 10;

    // Use this for initialization
    void Start () {
        transform.parent = null;
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null || Camera.main == null)
        {
            return; //Code under will only work if there is a target
        }

        Debug.Log(Input.mousePosition);
        Vector3 midPoint = (Camera.main.ScreenToWorldPoint(Input.mousePosition) + target.transform.position) / 2;

        // Camera clamping
        Vector3 clampedPos = new Vector3(Mathf.Clamp(midPoint.x, (target.position.x - xPosRestriction), (target.position.x + xPosRestriction)), 14, Mathf.Clamp(midPoint.y, (target.position.y - yPosRestriction), (target.position.y + yPosRestriction)));

        transform.position = clampedPos;
    }
}
