using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trap : NetworkBehaviour
{
    public float stunDuration = 5;
    public float damage = 10;
    public string otherTeam = "Player";

    public void setSide(int Team)
    {
        if(Team == 1)
        {
            otherTeam = "Theif";
        }
        else if (Team == 2)
        {
            otherTeam = "Cop";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TrapHit");
        CheckPlayerCollision(collision);
    }
    void CheckPlayerCollision(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
                playerHealth.Damage(damage);

        }
    }

}
