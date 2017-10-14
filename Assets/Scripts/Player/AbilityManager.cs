using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    bool Ability1Active = true;
    float Ability1ReloadTime;

    bool Ability2Active = true;
    float Ability2ReloadTime;

    bool Ability3Active = true;
    float Ability3ReloadTime;

    bool Ability4Active = true;
    float Ability4ReloadTime;

    // Use this for initialization
    void Start () {
		
	}
    public void RobberAbilities()
    {
        if(Input.GetButtonDown("Ability1"))
        {
            Debug.Log("Robber Ability1 Pressed");
        }
        if (Input.GetButtonDown("Ability2"))
        {
            Debug.Log("Robber Ability2 Pressed");
        }
        if (Input.GetButtonDown("Ability3"))
        {
            Debug.Log("Robber Ability3 Pressed");
        }
        if (Input.GetButtonDown("Ability4"))
        {
            Debug.Log("Robber Ability4 Pressed");
        }
    }
    public void CopAbilities()
    {
        if (Input.GetButtonDown("Ability1"))
        {
            //Set Range
            float Range = 10f;
            //Fire Taser
            if (Ability1Active)
            {
                //Set the Ability Reload Time
                Ability1ReloadTime = Time.time + 10;

                //Send Debug log Message
                Debug.Log("Cop Ability1 Pressed");
                Debug.Log("Fired Time Wait is" + Ability1ReloadTime + "");

                //Make Ability unactive
                Ability1Active = false;

                //Do A Raycast - Does f all atm really
                RaycastHit hit;
                Vector3 direction = transform.TransformDirection(Vector3.forward);

                if (Physics.Raycast(transform.position, direction, out hit, Range))
                {
                    string name = hit.transform.tag;
                }
            }
            else
            {
                //Send Debug log Message
                Debug.Log("Cop Ability1 Can't be Pressed");
            }

            //Ability Reload after set time make ability reusiable again
            if (Time.time > Ability1ReloadTime)
            {
                Ability1Active = true;
            }

        }
        if (Input.GetButtonDown("Ability2"))
        {
            Debug.Log("Cop Ability2 Pressed");
        }
        if (Input.GetButtonDown("Ability3"))
        {
            Debug.Log("Cop Ability3 Pressed");
        }
        if (Input.GetButtonDown("Ability4"))
        {
            Debug.Log("Cop Ability4 Pressed");
        }
    }

}
