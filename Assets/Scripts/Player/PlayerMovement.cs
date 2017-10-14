using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float m_moveSpeed = 100f;

    Rigidbody m_rigidbody;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector3 dir)
    {
        Vector3 moveDirection = dir * m_moveSpeed * Time.deltaTime;
        m_rigidbody.velocity = moveDirection;

    }
}
