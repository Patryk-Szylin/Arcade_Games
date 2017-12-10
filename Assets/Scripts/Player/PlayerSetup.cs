using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class PlayerSetup : NetworkBehaviour
{
    [SyncVar(hook = "UpdateColour")] public Color m_playerColor;
    [SyncVar(hook = "UpdateName")] public string m_playerName;

    public Text m_playerNameText;
    public Camera cam;

    // this function is invoked when client has connected to the server


    private void Update()
    {
        // TODO: NOT IN UPDATE
        if (isLocalPlayer)
            CameraScript.Instance.Setup(this.transform);

        if (!isLocalPlayer)
        {
            cam.enabled = false;
            return;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!isServer)
        {
            Player p = GetComponent<Player>();

            if (p != null)
            {
                GameManager.m_allPlayers.Add(p);
            }
        }

        UpdateColour(m_playerColor);
        UpdateName(m_playerName);
    }

    private void UpdateName(string playerName)
    {
        if (m_playerNameText != null)
        {
            m_playerNameText.enabled = true;
            m_playerNameText.text = playerName;
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
}
