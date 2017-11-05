using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {

    [Header("Projectile Options")]
    [HideInInspector] public Vector3 m_velocity;    // This value is hidden and will be set during initilisation process of Ability_ scripts
    [HideInInspector] public float m_damage;   // This value is hidden and will be set during initilisation process of Ability_ scripts
    public Rigidbody m_prefab;
    [HideInInspector] public Transform m_spawnPos;  // This value is hidden and will be set during initilisation process of Ability_ scripts

    // TODO:
    //public ParticleSystem m_explosionFX;
    //public AudioSource m_effectSound;
    //public AudioSource m_ExplosionSound;

    private Collider m_collider;
    private Rigidbody m_rigidBody;


	// Use this for initialization
	void Start ()
    {
        m_collider = GetComponent<Collider>();
        m_rigidBody = GetComponent<Rigidbody>();
	}

    public void Launch()
    {      
        Rigidbody rbody = Instantiate(m_prefab, m_spawnPos.position, m_spawnPos.rotation) as Rigidbody;
        if (rbody != null)
        {
            rbody.velocity = m_velocity;
            NetworkServer.Spawn(rbody.gameObject);        
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("HUDAHDUSHD");
        Destroy(this.gameObject);
    }

}
