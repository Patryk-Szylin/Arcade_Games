using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberBaseClass : MonoBehaviour {


    bool Ability1Active = true;
    float Ability1ReloadTime;

    bool Ability2Active = true;
    float Ability2ReloadTime;

    bool Ability3Active = true;
    float Ability3ReloadTime;

    bool Ability4Active = true;
    float Ability4ReloadTime;

    // Use this for initialization
    void Start()
    {

    }
    private void Update()
    {
        AbilitiesInput();
    }
    public void AbilitiesInput()
    {
        if (Input.GetButtonDown("Ability1"))
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
}
