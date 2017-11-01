using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Test_Ability : NetworkBehaviour
{
    public float m_range = 50f;
    public float m_healAmount = 1;
    public ParticleSystem m_healEffect;


    public void ShootAbility()
    {
        //RpcSpawnHealFX();
        print("Shot by : " + this.gameObject.name);
        CmdHealPlayer();


    }

    [ClientRpc]
    void RpcSpawnHealFX()
    {
        var pc = GetPlayerOnHit(m_range);

        var healFx = Instantiate(m_healEffect, pc.transform.position, Quaternion.identity);
        
        NetworkServer.Spawn(healFx.gameObject);
    }


    [Command]
    void CmdHealPlayer()
    {
        if (!isLocalPlayer)
            return;

        var pc = GetPlayerOnHit(m_range);
        var playerHealth = pc.GetComponent<PlayerHealth>();

        playerHealth.m_currentHealth += m_healAmount;
    }








    public PlayerController GetPlayerOnHit(float range)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.GetComponent<PlayerController>())
            {
                var pc = hit.collider.GetComponent<PlayerController>();
                return pc;
            }
        }

        return null;
    }
}
