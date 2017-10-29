using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Thief_1_Mid : Abilities
{
    [Header("Throwing Knife/Axe")]
    [Space]
    [Range(0, 10)]
    public float cooldown = 1.25f;
    [Range(0, 10)]
    public float range = 2;
    [Range(0, 10)]
    private float delayTime;
    [Range(0, 10)]
    public float Damage = 5f;

    [Header("Trap")]
    [Space]
    [Range(0, 10)]
    public float cooldown2 = 1.25f;
    [Range(0, 10)]
    public float Damage2 = 5f;

    [Header("Bat/Axe")]
    [Space]
    [Range(0, 10)]
    public float cooldown3 = 1.25f;
    [Range(0, 10)]
    public float Damage3 = 5f;
    [Range(0, 3)]
    public float range2 = 0.002f;


    // Use this for initialization
    void Awake()
    {
        enemiesGO = GameObject.FindGameObjectsWithTag("Thief");

    }

    // Update is called once per frame
    void Update()
    {
        // -----Knife-----
        if (cooldownTime > 0 )
        {
            cooldownTime -= Time.deltaTime;

            ability_1();
        }

        // -----Bear Trap----
        if (cooldownTime_2 > 0)
        {
            cooldownTime_2 -= Time.deltaTime;

            ability_2();
        }

        // -----Pepper Spray----
        if (cooldownTime_3 > 0)
        {
            cooldownTime_3 -= Time.deltaTime;

            ability_3();
        }

    }
    public override void ability_1()
    {
        if (cooldownTime <= 0)
        {
            Debug.Log("Theif Ability 1 Fired");

            // Raycast
            RaycastHit hit;
            Vector3 direction = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (hit.transform.tag == "Player")
                {
                    CmdThrowingAxeHit(hit.collider.gameObject);
                }
            }
        }
    }

    // -----Flash Bang-----
    public override void ability_2()
    {
        if (cooldownTime_2 <= 0)
        {
            
            cooldownTime_2 = cooldown2;
        }
    }

    // -----Pepper Spray-----
    public override void ability_3()
    {
        Debug.Log("Theif Ability 3 Fired");

        // Raycast
        RaycastHit hit;
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, direction, out hit, range2))
        {
            if (hit.transform.tag == "Player")
            {
                CmdAxeHit(hit.collider.gameObject);
            }
        }
    }

    [Command]
    void CmdThrowingAxeHit(GameObject playerHit)
    {
        playerHit.GetComponent<PlayerHealth>().Damage(Damage);
    }

    [Command]
    void CmdAxeHit(GameObject playerHit)
    {
        playerHit.GetComponent<PlayerHealth>().Damage(Damage3);
    }
}
