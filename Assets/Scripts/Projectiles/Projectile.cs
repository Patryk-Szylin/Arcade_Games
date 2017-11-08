﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 
/// @ Description:              Base class for all projectiles. 
/// 
///                             All public values are hidden in the inspector, to avoid confusion from Game Designer point of view. 
///                             Anyway, these values are being set during initialisation process within classes that derive from <para Ability.cs>
/// 
/// 
/// <para name="m_velocity">    Direction and force of a projectile, set during initialisation  </para>
/// <para name="m_spawnPos">    It's being set in <para PlayerCast.cs> when casting an ability.
/// <para name="m_prefab">      This prefab is getting set during Initialisation process
/// 
/// 
/// </summary>


public abstract class Projectile : NetworkBehaviour {

    [Header("Projectile Options")]
    [HideInInspector] public Vector3 m_velocity;    // This value is hidden and will be set during initilisation process of Ability_ scripts
    [HideInInspector] public Rigidbody m_prefab;
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
