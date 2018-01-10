using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Camera_Move : MonoBehaviour {
    public Camera CameraM;
    public List<Transform> Menu_Camera = new List<Transform>();
    public int i = 0;
    private float animSpeed = 5f;
    private Vector3 pos;
    private Quaternion rot;
    private float journeyLength;
    private float startTime;
    private bool nextStart = true;
    public float fracJourney;
    // Use this for initialization
    private void Start()
    {
        pos = CameraM.transform.position;
        rot = CameraM.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
        //SwitchCamera();
        if(i >= Menu_Camera.Count)
        {
            i = 0;
        }
        if(nextStart)
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(pos, Menu_Camera[i].position);
            nextStart = false;
        }
        float distCovered = (Time.time - startTime) * animSpeed;
        fracJourney = distCovered / journeyLength;
        CameraM.transform.position = Vector3.Lerp(pos, Menu_Camera[i].position, fracJourney);
        CameraM.transform.rotation = Quaternion.Lerp(rot, Menu_Camera[i].transform.rotation, fracJourney);
        if(fracJourney > 0.9)
        {
            i++;
            nextStart = true;
            fracJourney = 0;
            pos = CameraM.transform.position;
            rot = CameraM.transform.rotation;
        }

    }
}
