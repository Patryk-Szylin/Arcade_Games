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
        float translation = dir.z * m_moveSpeed;
        float straffe = dir.x * m_moveSpeed;
        if (translation != 0 && straffe != 0)
        {
            translation = translation/1.8f;
            straffe = straffe/1.8f;
        }
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe,0, translation);

    }
}
