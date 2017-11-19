using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float m_baseMoveSpeed = 500f;

    public float m_currentMoveSpeed = 500f;
    public bool m_isBoosted = false;

    Rigidbody m_rigidbody;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void MovePlayer(Vector3 dir)
    {       

        if(dir.x == 1 && dir.z == 1)
        {
            dir = dir.normalized;
        }

        Vector3 moveDirection = dir * m_currentMoveSpeed * Time.deltaTime;
        m_rigidbody.velocity = moveDirection;
    }



    public IEnumerator ApplyBoost(float multiplier, float duration)
    {
        m_currentMoveSpeed = 1000f;
        yield return new WaitForSeconds(duration);
        m_currentMoveSpeed = m_baseMoveSpeed;
    }

}
