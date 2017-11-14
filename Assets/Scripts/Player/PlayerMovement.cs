using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float m_moveSpeed = 10f;
    public float m_Boosted = 1;
    private void Start()
    {
    }

    public void MovePlayer(Vector3 dir)
    {
        //m_moveSpeed = m_moveSpeed* m_Boosted;
        dir = dir.normalized;
        dir = dir * m_moveSpeed;
        dir *= Time.deltaTime;
        transform.Translate(dir);

    }
}
