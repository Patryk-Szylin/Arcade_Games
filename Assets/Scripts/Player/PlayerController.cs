using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCast))]
[RequireComponent(typeof(PlayerHealth))]

public class PlayerController : NetworkBehaviour
{
    [Header("Player's Specific")]
    public int m_score;
    public bool m_isHiding = false;

    PlayerSetup m_pSetup;
    PlayerMovement m_pMovement;
    PlayerCast m_pCast;
    PlayerHealth m_pHealth;

    private void Start()
    {
        m_pSetup = GetComponent<PlayerSetup>();
        m_pMovement = GetComponent<PlayerMovement>();
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_pSetup.m_playerNameText.text = gameObject.name;
            m_pCast.CastSpell_01();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_pCast.CastSpell_02();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_pCast.CastSpell_03();
        }


    }


    private void FixedUpdate()
    {
        if (!isLocalPlayer || m_pHealth.m_isDead)
            return;

        Vector3 inputDir = GetInput();
        m_pMovement.MovePlayer(inputDir);
    }

    Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v);
    }


    // This function is getting caller
    [ClientRpc]
    public void RpcHidePlayer(bool state)
    {
        // If I am the local player, then don't hide me.
        if (isLocalPlayer)
            return;

        // Otherwise, hide All other players that are in the bush
        var uis = GetComponentsInChildren<Canvas>();

        foreach (Canvas ui in uis)
        {
            ui.enabled = state;
        }

        MeshRenderer r = this.GetComponent<MeshRenderer>();
        r.enabled = state;

    }
}

