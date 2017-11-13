using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash_Bang : MonoBehaviour
{

    [Header("Stats")]
    private float radius; // Provides a radius at which the explosive will effect rigidbodies
    private float defaultRadius;
    private float explosiveDelay = 5; // Adds a delay in seconds to our explosive object
    private SphereCollider blast;

    void Awake()
    {
        blast = gameObject.GetComponent<SphereCollider>();
        defaultRadius = blast.radius;
    }

    // Update is called once per frame
    void Update()
    {
        explosiveDelay -= Time.deltaTime;
        // Count down till grenade explosion
        if (explosiveDelay < 0)
        {
            // Instantiate/Create the Explosion particles at grenade location
            //Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Get genade collider and incress it's radius 
            blast.radius = 5f;
            blast.isTrigger = true;

            Debug.Log("BOOM!");

            // Destroy genade
            Destroy(this.gameObject);
        }
    }

    // Check if genade explosion hit anything
    void OnTriggerEnter(Collider other)
    {
        // Check if it hit a target and deal damage to it - play explosionSound
        if (other.transform.tag == "Enemy")
        {
            // DO SOMETHING TO THIEF!
            //other.GetComponent<Thief_Target_Dummy_Test>().stun(stunDuration);
        }
    }

    public void reset(float delay, float hitBoxRadius) //change to spawn
    {
        blast.radius = defaultRadius;
        blast.isTrigger = false;
        radius = hitBoxRadius;
        explosiveDelay = delay;
    }
}
