using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Cop_1_Abilities : Abilities
{
    private AudioSource source;

    [Header("Taser Variables")]
    [Space]
    [Range(0, 10)]
    public float cooldown = 2.5f;
    [Range(0, 10)]
    public float range = 10.0f;
    [Range(0, 10)]
    public float castTime = 1.0f;
    private bool castDelayed = false;
    private float delayTime;
    [Range(0, 10)]
    public float stunDuration = 2.5f;

    [Header("Flash Bang Variables")]
    [Space]
    public GameObject flashBangPrefab;
    private GameObject flashBangClone;
    private Flash_Bang flashBangScript;
    [Range(0, 10)]
    public float cooldown_2 = 4.0f;
    [Range(0, 10)]
    public float projectileRange;
    [Range(0, 10)]
    public float projectileSpeed = 5.0f;
    [Range(0, 10)]
    public float flashDelay = 5.0f;
    [Range(0, 10)]
    public float blastRadius = 5.0f;
    [Range(0, 10)]
    public float distortDuration;

    [Header("Pepper Spray Variables")]
    [Space]
    [Range(0, 10)]
    public float cooldown_3 = 2.0f;
    [Range(0, 10)]
    public float range_3 = 5.0f;
    private float angle;

    public GameObject testObject;

    //[Header("Ability 4 Variables")]

    void Awake()
    {
        source = GetComponent<AudioSource>();
        enemiesGO = GameObject.FindGameObjectsWithTag("Thief");
    }

    void Update()
    {
        // -----Taser-----
        if (cooldownTime > 0 || cooldown == 0)
        {
            cooldownTime -= Time.deltaTime;
            delayTime -= Time.deltaTime;

            // Cast Time
            if (delayTime < 0 && castDelayed == true)
                ability_1();
        }

        // -----Flash Bang----
        if (cooldownTime_2 > 0)
            cooldownTime_2 -= Time.deltaTime;

        // -----Pepper Spray----
        if (cooldownTime_3 > 0)
            cooldownTime_3 -= Time.deltaTime;
    }


    // -----Taser-----
    public override void ability_1()
    {
        if (cooldownTime <= 0 && !castDelayed)
        {
            //source.PlayOneShot(abilitySound, 0.5f);
            castDelayed = true;
            Debug.Log("Cop Ability 1 Cast Delayed");
            delayTime = castTime;
            //STOP PLAY MOVEMENT WHILE CASTING
            cooldownTime = cooldown;
        }
        else if (castDelayed && delayTime < 0)
        {
            castDelayed = false;
            Debug.Log("Cop Ability 1 Fired");

            // Raycast
            RaycastHit hit;
            Vector3 direction = transform.TransformDirection(Vector3.right);

            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (hit.transform.tag == "Thief")
                {
                    Debug.Log("Cop Ability 1 Hit!");
                    CmdTaserHit(hit.collider.gameObject);
                }
            }
        }
    }

    // -----Flash Bang-----
    public override void ability_2()
    {
        if (cooldownTime_2 <= 0)
        {
            Debug.Log("Cop Ability 2 Fired");
            CmdFireFlashBang();
            cooldownTime_2 = cooldown_2;
        }
    }

    // -----Pepper Spray-----
    public override void ability_3()
    {
        if (cooldownTime_3 <= 0)
        {
            Debug.Log("Cop Ability 3 Fired");
            //Check if Any enemy is in range
            foreach (GameObject thief in enemiesGO)
            {
                float dist = Vector3.Distance(thief.transform.position, transform.position);
                if (dist < range_3)
                {
                    Debug.Log("IN RANGE" + thief.gameObject.name);
                    // If yes check if they are in the AOE
                    Vector3 forward = transform.TransformDirection(Vector3.right);
                    Vector3 targetDir = thief.transform.position - transform.position;
                    angle = Vector3.Angle(targetDir, Vector3.forward);

                    //<-- = 90 V = 180, --> = 90 /\ = 0
                    //Change to transforms for designers
                    float hitAngle = 90;
                    if (angle < hitAngle)
                    {
                        Debug.Log("hit!");
                        CmdPepperSprayHit(thief);
                    }
                }
            }
            cooldownTime_3 = cooldown_3;
        }
    }

    Vector3 BallisticVel(Transform target, float angle)
    {
        Vector3 dir = target.position - transform.position; // get target direction 
        float h = dir.y; // get height difference 
        dir.y = 0; // retain only the horizontal direction 
        float dist = dir.magnitude; // get horizontal distance 
        float a = angle * Mathf.Deg2Rad; // convert angle to radians 
        dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle 
        dist += h / Mathf.Tan(a); // correct for small height differences 
        // calculate the velocity magnitude 
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

    [Command]
    void CmdTaserHit(GameObject playerHit)
    {
        playerHit.GetComponent<PlayerMovement>().stun(stunDuration);
    }

    [Command]
    void CmdFireFlashBang()
    {
        // Create the Bullet from the Bullet Prefab
        flashBangClone = (GameObject)Instantiate(flashBangPrefab,
        projectilePoint.transform.position, projectilePoint.transform.rotation);

        flashBangScript = flashBangClone.GetComponent<Flash_Bang>();
        flashBangScript.reset(flashDelay, blastRadius);

        flashBangClone.GetComponent<Rigidbody>().velocity = BallisticVel(testObject.transform, 60);

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(flashBangClone);
    }

    [Command]
    void CmdPepperSprayHit(GameObject playerHit)
    {
        playerHit.GetComponent<PlayerMovement>().stun(stunDuration);
        playerHit.GetComponent<PlayerHealth>().Damage(1);
    }
}

