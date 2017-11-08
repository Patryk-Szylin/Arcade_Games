using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cop_Sniper_Abilities : Abilities
{
    private AudioSource source;

    [Header("High Caliber Variables")]
    [Space]
    public GameObject sniperProjectilePrefab;
    private GameObject sniperProjectileClone;
    private sniper_Projectile sniperProjectileScript;
    //[Range(0, 10)]
    //public float cooldown_1 = 4.0f;

    [Header("Taser Variables")]
    [Space]
    //[Range(0, 10)]
    //public float cooldown_2 = 2.5f;
    [Range(0, 10)]
    public float range = 10.0f;
    [Range(0, 10)]
    public float castTime = 1.0f;
    private bool castDelayed = false;
    private float delayTime;
    [Range(0, 10)]
    public float stunDuration = 2.5f;

    [Header("Conceal Variables")]
    [Space]
    //[Range(0, 10)]
    //public float cooldown_3 = 2.0f;
    [Range(0, 10)]
    public float stealth_Duration;

    //[Header("Mega Snipe Variables")]
    //[Space]
    //[Range(0, 10)]
    //public float cooldown_4 = 30.0f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        enemiesGO = GameObject.FindGameObjectsWithTag("Thief");
    }

    void Update()
    {
        // -----High Caliber-----
        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;

        // -----Taser----
        if (cooldownTime_2 > 0)
            cooldownTime_2 -= Time.deltaTime;

        // -----Pepper Spray----
        if (cooldownTime_3 > 0)
            cooldownTime_3 -= Time.deltaTime;

        // -----Mega Snipe----
        if (cooldownTime_4 > 0)
            cooldownTime_4 -= Time.deltaTime;
    }

    // -----High Caliber-----
    public override void ability_1()
    {
        if (cooldownTime <= 0)
        {
            Debug.Log("Cop Ability 1 Fired");
            CmdFireHighCaliber();
            cooldownTime = cooldown_1;
        }
    }


    // -----Taser-----
    public override void ability_2()
    {
        if (cooldownTime_2 <= 0)
        {
            Debug.Log("Cop Sniper Ability 2 Fired");

            // Raycast
            RaycastHit hit;
            Vector3 direction = mouseLocation.transform.position - transform.position;
            //Vector3 direction = transform.TransformDirection(mouseLocation.transform.position);

            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (hit.transform.tag == "Thief" || hit.transform.tag == "Enemy")
                {
                    Debug.Log("Cop Sniper Ability 2 Hit!");
                    CmdTaserHit(hit.collider.gameObject);
                }
            }

            cooldownTime_2 = cooldown_2;
        }
    }

    // -----Conceal-----
    public override void ability_3()
    {
        if (cooldownTime_3 <= 0)
        {
            Debug.Log("Cop Sniper Ability 3 Stealth");
            gameObject.GetComponent<Player>().RpcHidePlayer(true);
            // ADD TIMER
            cooldownTime_3 = cooldown_3;
        }
    }

    // -----Mega Snipe-----
    public override void ability_4()
    {

    }

    [Command]
    void CmdFireHighCaliber()
    {
        // Create the Bullet from the Bullet Prefab
        sniperProjectileClone = (GameObject)Instantiate(sniperProjectilePrefab,
        projectilePoint.transform.position, projectilePoint.transform.rotation);

        sniperProjectileScript = sniperProjectileClone.GetComponent<sniper_Projectile>();
        //sniperProjectileScript.reset(flashDelay, blastRadius);

        // Add velocity to the bullet
        sniperProjectileClone.GetComponent<Rigidbody>().velocity = sniperProjectileClone.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(sniperProjectileClone);
    }

    [Command]
    void CmdTaserHit(GameObject playerHit)
    {
        playerHit.GetComponent<PlayerMovement>().stun(stunDuration);
        playerHit.GetComponent<PlayerHealth>().Damage(1);
    }
}
