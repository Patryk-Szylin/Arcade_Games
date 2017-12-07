using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCast))]
[RequireComponent(typeof(PlayerHealth))]

public class Player : NetworkBehaviour
{
    [Header("Player's Specific")]
    [SyncVar]
    public int m_score;
    [SyncVar]
    public int m_kills;
    [SyncVar]
    public int m_deaths;


    public bool m_isHiding = false;
    public Dictionary<int, bool> m_hidingInBush = new Dictionary<int, bool>();
    public float m_respawnTime;

    public PlayerSetup m_pSetup;
    PlayerMovement m_pMovement;
    PlayerCast m_pCast;
    PlayerHealth m_pHealth;

    public Camera minicam;

    private void OnDestroy()
    {
        GameManager.m_allPlayers.Remove(this);
    }

    private void Start()
    {
        m_pSetup = GetComponent<PlayerSetup>();
        m_pMovement = GetComponent<PlayerMovement>();
        m_pCast = GetComponent<PlayerCast>();
        m_pHealth = GetComponent<PlayerHealth>();

    }


    private void Update()
    {

        if (m_isHiding)
            RpcHidePlayer(true);

        if (!isLocalPlayer || m_pHealth.m_isDead)
            return;

        if (Input.GetKey(KeyCode.Tab))
            UI_Scoreboard.Instance.ShowScoreboard();
        else
            UI_Scoreboard.Instance.HideScoreboard();


        // Check for ability input
        CheckForAbilityInput();

    }


    public void CheckForAbilityInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_pCast.CastAbility(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_pCast.CastAbility(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_pCast.CastAbility(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_pCast.CastAbility(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_pCast.CastAbility(4);
        }


    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer || m_pHealth.m_isDead)
            return;

        Vector3 inputDir = GetInput();
        m_pMovement.MovePlayer(inputDir);


        if (m_hidingInBush.Count != 0)
        {
            // If the players have the same bush ID, turn on their meshes just for them.

        }
    }

    Vector3 GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        return new Vector3(h, 0, v);
    }

    IEnumerator Respawn()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        yield return new WaitForSeconds(m_respawnTime);
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        m_pHealth.Reset();
        m_pCast.Reset();
    }

    void Disable()
    {
        StartCoroutine("Respawn");
    }

    // This function is getting called inside Bush.cs
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

