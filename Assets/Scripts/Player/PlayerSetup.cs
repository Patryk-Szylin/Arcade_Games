using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class PlayerSetup : NetworkBehaviour
{
    [SyncVar(hook ="UpdateColour")]
    public Color m_playerColor;
    public string m_playerName = "Player ";

    [SyncVar(hook = "UpdateName")]
    public int m_id = 1;
    public Text m_playerNameText;



    // Start runs after "OnStartLocalPlayer"
    private void Start()
    {
        if(!isLocalPlayer)
        {
            UpdateColour(m_playerColor);
            UpdateName(m_id);
        }
    }

    // Runs before OnStartLocalPlayer
    public override void OnStartClient()
    {
        base.OnStartClient();

        if (m_playerNameText != null)
            m_playerNameText.enabled = false;
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        CmdSetupPlayer();
        
    }

    private void UpdateName(int playerNum)
    {
        if (m_playerNameText != null)
        {
            m_playerNameText.enabled = true;
            m_playerNameText.text = m_playerName + playerNum.ToString();
        }
    }

    private void UpdateColour(Color playerColor)
    {
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer r in meshes)
        {
            r.material.color = playerColor;
        }
    }


    [Command]
    void CmdSetupPlayer()
    {
        GameManager.Instance.AddPlayer(this);
        GameManager.Instance.m_playerCount++;
    }

}
