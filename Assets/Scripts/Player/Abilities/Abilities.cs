using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    [SerializeField]
    public Cop_1_Abilities abilities;

    public GameObject indicator_ability_1;

    // Use this for initialization
    void Start () {
        abilities = GetComponent<Cop_1_Abilities>();

        // Instantiate Abilities indicators
        // Set as child
        indicator_ability_1 = Instantiate(abilities.indicator);//as GameObject;
        indicator_ability_1.SetActive(false);
    }

    // Update is called once per frame
    void Update () {

        //Abilities indicators pos 
        indicator_ability_1.transform.position = transform.position;
        //indicator_ability_1.transform.rotation = transform.rotation;

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            //indicator
            if (abilities.quickCast)
            {
                abilities.ability_1();
            }
            else
            {
                indicator_ability_1.SetActive(true);
            }
        }
       else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilities.ability_2();
        }
       else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilities.ability_3();
        }
       else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            abilities.ability_4();
        }
    }
}
