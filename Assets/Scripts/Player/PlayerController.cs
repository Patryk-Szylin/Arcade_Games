using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCast))]
[RequireComponent(typeof(AbilityManager))]

public class PlayerController : NetworkBehaviour
{
    [Header("Player's Specific")]
    public int m_score;
    public bool Cop;
    public bool Robber;

    PlayerSetup m_pSetup;
    PlayerMovement m_pBody;
    PlayerCast m_pCast;
    PlayerHealth m_pHealth;
    AbilityManager m_pAbility;

    private void Start()
    {
        m_pSetup = GetComponent<PlayerSetup>();
        m_pBody = GetComponent<PlayerMovement>();
        m_pCast = GetComponent<PlayerCast>();
        m_pHealth = GetComponent<PlayerHealth>();
        m_pAbility = GetComponent<AbilityManager>();

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
        if(Cop)
        {
            m_pAbility.CopAbilities();
        }
        else
        {
            m_pAbility.RobberAbilities();
        }

    }

    Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v);

    }

}
