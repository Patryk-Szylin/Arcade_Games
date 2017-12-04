using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float m_moveSpeed = 10f;
    public float m_Boosted = 1;
    public Vector3 Test;
    public Vector3 Test2;
    private void Start()
    {
    }

    public void MovePlayer(Vector3 dir)
    {
        Test = dir;
        //m_moveSpeed = m_moveSpeed* m_Boosted;
        if (dir.x == 1 && dir.z == 1 || dir.x == 1 && dir.z == -1 || dir.x == -1 && dir.z == 1 || dir.x == -1 && dir.z == -1)
        {
            dir = dir.normalized;
        }
        dir = dir * m_moveSpeed;
        dir *= Time.deltaTime;
        Test2 = dir;
        transform.Translate(dir);

    }
}
