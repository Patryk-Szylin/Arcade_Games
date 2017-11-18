using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCast : NetworkBehaviour
{
    public Rigidbody m_projectilePrefab;
    public Transform m_projectileSpawn;
    public List<Ability> m_abilities = new List<Ability>();

    [Header("Shooting Specific")]
    public float m_reloadTime = 1f;                     // needs to be moved to an ability
    public bool m_isReloading = false;


    PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Cmd_Cast_01(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cmd_Cast_01(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Cmd_Cast_01(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Cmd_Cast_04();
        }
    }

    public void Cast()
    {
        //CmdSpawnProjectile();

        StartCoroutine("Reload");
    }

    [Command]
    public void Cmd_Cast_01(int i)
    {
        m_abilities[i].Initilise(m_projectileSpawn, transform.name);
        m_abilities[i].TriggerAbility();
    }

    // Special Ability
    [Command]
    public void Cmd_Cast_04()
    {
        if(m_abilities[3] != null)
        {
            m_abilities[3].Initilise(m_projectileSpawn, transform.name);
            m_abilities[3].TriggerAbility();
        }
    }


    IEnumerator Reload()
    {
        m_isReloading = true;
        yield return new WaitForSeconds(m_reloadTime);
        m_isReloading = false;
    }
}
