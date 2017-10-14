using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCast))]

public class PlayerController : NetworkBehaviour
{
    [Header("Player's Specific")]
    public int m_score;


    PlayerSetup m_pSetup;
    PlayerMovement m_pBody;
    PlayerCast m_pCast;
    PlayerHealth m_pHealth;

    private void Start()
    {
        m_pSetup = GetComponent<PlayerSetup>();
        m_pBody = GetComponent<PlayerMovement>();
        m_pCast = GetComponent<PlayerCast>();
        m_pHealth = GetComponent<PlayerHealth>();
    }


    private void Update()
    {
        if (!isLocalPlayer || m_pHealth.m_isDead)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && m_pCast.m_isReloading == false)
        {
            m_pCast.Cast();
        }
    }


    private void FixedUpdate()
    {
        if (!isLocalPlayer || m_pHealth.m_isDead)
            return;

        Vector3 inputDir = GetInput();
        m_pBody.MovePlayer(inputDir);

    }

    Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v);

    }
}
