using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    public Color m_playerColor;
    public int m_id = 1;
    public string m_playerName = "Player ";
    public Text m_playerNameText;


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

        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer r in meshes)
        {
            r.material.color = m_playerColor;
        }

        if(m_playerNameText != null)
        {
            m_playerNameText.enabled = true;
            m_playerNameText.text = m_playerName + m_id.ToString();
        }

    }



}
