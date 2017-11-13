using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caltrops : MonoBehaviour
{
    // Check if genade explosion hit anything
    void OnTriggerEnter(Collider other)
    {
        // Check if it hit a target and deal damage to it - play explosionSound
        if (other.transform.tag == "Thief")
        {
            // DO SOMETHING TO THIEF!
            other.GetComponent<Thief_Target_Dummy_Test>().slow(10, 0);
        }
    }
}
