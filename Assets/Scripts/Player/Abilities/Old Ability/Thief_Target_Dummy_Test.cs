using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Thief_Target_Dummy_Test : NetworkBehaviour
{

    private Vector3 pointA = new Vector3(-4, 0, 5);
    private Vector3 pointB = new Vector3(4, 0, 5);
    public float speed = 1.0f;
    private float currentSpeed = 1.0f;

    private bool stunned;
    private float stunnedDuration;

    private bool slowed;
    private float slowedDuration;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }

        if (stunned)
        {
            stunnedDuration -= Time.deltaTime;
            if (stunnedDuration < 0)
                stunned = false;
        }
        else
        {
            transform.position = Vector3.Lerp(pointA, pointB, (Mathf.Sin(currentSpeed * Time.time) + 1.0f) / 2.0f);
        }

        if (slowed)
        {
            slowedDuration -= Time.deltaTime;
            if (slowedDuration < 0)
                currentSpeed = speed;
        }


        slowedDuration -= Time.deltaTime;
    }

    public void stun(float duration)
    {
        stunned = true;
        stunnedDuration = duration;
    }

    public void slow(float slowAmount, float duration)
    {
        Debug.Log("SLOWED");
        currentSpeed = 0.2f; //change because this sucks

        if (duration != 0)
            slowedDuration = duration;
    }
}
