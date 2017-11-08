using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float m_moveSpeed = 100f;

    Rigidbody m_rigidbody;

    private float currentSpeed = 1.0f;

    // Status Effects
    private bool stunned;
    private float stunnedDuration;

    private bool slowed;
    private float slowedDuration;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector3 dir)
    {
        if (stunned)
        {
            stunnedDuration -= Time.deltaTime;
            if (stunnedDuration < 0)
                stunned = false;
        }
        else
        {
            Vector3 moveDirection = dir * m_moveSpeed * Time.deltaTime;
            m_rigidbody.velocity = moveDirection;
        }

        if (slowed)
        {
            slowedDuration -= Time.deltaTime;
            if (slowedDuration < 0)
                currentSpeed = m_moveSpeed;
        }

        slowedDuration -= Time.deltaTime;
    }

    // Status Effects
    public void stun(float duration)
    {
        stunned = true;
        stunnedDuration = duration;
    }

    public void slow(float slowAmount, float duration)
    {
        currentSpeed = 0.2f; //change because this sucks

        if (duration != 0)
            slowedDuration = duration;
    }
}
