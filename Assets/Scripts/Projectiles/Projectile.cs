using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Projectile : NetworkBehaviour {

    [Header("Projectile Options")]
    [HideInInspector] public Vector3 m_velocity;    // This value is hidden and will be set during initilisation process of Ability_ scripts
    public Rigidbody m_prefab;
    [HideInInspector] public Transform m_spawnPos;  // This value is hidden and will be set during initilisation process of Ability_ scripts
    [HideInInspector] public GameObject m_impactFX;

    private Collider m_collider;
    private Rigidbody m_rigidBody;

	// Use this for initialization
	void Start ()
    {
        m_collider = GetComponent<Collider>();
        m_rigidBody = GetComponent<Rigidbody>();
	}

    public abstract void Launch();
    public abstract void OnCollisionHit(Collider other);
}
