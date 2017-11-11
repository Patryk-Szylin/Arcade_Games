using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Thief_1_Abilities : Abilities1
{
    private AudioSource source;

    [Header("Smoke Bomb Variables")]
    [Space]
    public GameObject smokeBombPrefab;
    private GameObject smokeBombClone;
    private Smoke_Bomb smokeBombScript;
    [Range(4, 10)]
    public float cooldown = 4.0f;
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

    [Header("Caltrops Variables")]
    [Space]
    public GameObject caltropsPrefab;
    private GameObject caltropsClone;
    [Range(4, 10)]
    public float cooldown_2 = 4.0f;


    void Awake()
    {
        source = GetComponent<AudioSource>();
        enemiesGO = GameObject.FindGameObjectsWithTag("Thief");

        // -----Smoke Bomb-----
        // Instantiate
        smokeBombClone = (GameObject)Instantiate(smokeBombPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        smokeBombClone.transform.parent = gameObject.transform;
        smokeBombClone.SetActive(false);
        smokeBombScript = smokeBombClone.GetComponent<Smoke_Bomb>();

        // -----Caltrops-----
        // Instantiate
        caltropsClone = (GameObject)Instantiate(caltropsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //caltropsClone.transform.parent = gameObject.transform;
        caltropsClone.SetActive(false);
        //caltropsClone = smokeBombClone.GetComponent<Smoke_Bomb>();
    }

    void Update()
    {
        // -----Smoke Bomb----
        if (cooldownTime > 0)
            cooldownTime -= Time.deltaTime;

        // -----Caltrops----
        if (cooldownTime_2 > 0)
            cooldownTime_2 -= Time.deltaTime;

        // -----Py----
        if (cooldownTime_3 > 0)
            cooldownTime_3 -= Time.deltaTime;
    }

    // -----Smoke Bomb-----
    public override void ability_1()
    {
        if (cooldownTime <= 0)
        {
            Debug.Log("Thief Ability 1 Fired");
            smokeBombClone.transform.position = projectilePoint.transform.position;
            smokeBombScript.reset(flashDelay, blastRadius);
            smokeBombClone.SetActive(true);
            smokeBombClone.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
            CmdFire();
            cooldownTime = cooldown;
        }
    }

    // -----Caltrops-----
    public override void ability_2()
    {
        if (cooldownTime_2 <= 0)
        {
            Debug.Log("Thief Ability 2 Placed!");
            caltropsClone.transform.position = transform.position;
            caltropsClone.SetActive(true);
            cooldownTime_2 = cooldown_2;
        }
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        GameObject smokeBombClone = (GameObject)Instantiate(smokeBombPrefab, projectilePoint.transform.position, projectilePoint.transform.rotation);

        // Add velocity to the bullet
        smokeBombClone.GetComponent<Rigidbody>().velocity = smokeBombClone.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(smokeBombClone);

        // Destroy the bullet after 2 seconds
        Destroy(smokeBombClone, 2.0f);
    }
}
