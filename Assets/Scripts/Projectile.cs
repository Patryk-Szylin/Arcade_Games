using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Projectile : NetworkBehaviour {

    [Header("Projectile Options")]
    public int m_speed = 50;
    public float m_lifeTime = 2f;
    public float m_damage = 1f;

    private Collider m_collider;
    private Rigidbody m_rigidBody;


	// Use this for initialization
	void Start ()
    {
        m_collider = GetComponent<Collider>();
        m_rigidBody = GetComponent<Rigidbody>();


        StartCoroutine("SelfDestruct");
	}

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(m_lifeTime);
        Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        CheckPlayerCollision(collision);

    }

    void CheckPlayerCollision(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
                playerHealth.Damage(m_damage);

            Destroy(gameObject);
        }
    }


}
