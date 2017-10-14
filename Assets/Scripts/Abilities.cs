using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    [SerializeField]
    public Cop_1_Abilities abilities;

	// Use this for initialization
	void Start () {
        abilities = GetComponent<Cop_1_Abilities>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            //indicator
            if (abilities.quickCast)
            {
                abilities.ability_1();


            }
            else
            {
                //indi
                //fire

            }
        }
       else if (Input.GetKeyDown(KeyCode.W))
        {
            abilities.ability_2();
        }
       else if (Input.GetKeyDown(KeyCode.E))
        {
            abilities.ability_3();
        }
       else if (Input.GetKeyDown(KeyCode.R))
        {
            abilities.ability_4();
        }
    }
}
