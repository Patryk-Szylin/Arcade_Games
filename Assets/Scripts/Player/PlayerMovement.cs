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
    private float slowedDuration1;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        currentSpeed = m_moveSpeed;
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
            Vector3 moveDirection = dir * currentSpeed * Time.deltaTime;
            m_rigidbody.velocity = moveDirection;
        }

        if (slowed)
        {
            slowedDuration1 -= Time.deltaTime;
            if (slowedDuration1 < 0)
            {
                slowed = false;
                currentSpeed = m_moveSpeed;
            }
        }
    }

    // Status Effects
    public void stun(float duration)
    {
        stunned = true;
        stunnedDuration = duration;
    }

    public void slow(float slowAmount, float duration)
    {
        currentSpeed = slowAmount; //change because this sucks
        slowed = true;
        slowedDuration1 = duration;
    }
}
