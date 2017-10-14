using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour {

    [SerializeField]
    public Cop_1_Abilities abilities;

    public GameObject[] indicator_ability;
    public int currentAbility;
    private int numOfAbilities = 4;

    private bool abilityTrigger = false;

    // Use this for initialization
    void Start () {
        abilities = GetComponent<Cop_1_Abilities>();
        indicator_ability = new GameObject[numOfAbilities];

        // Instantiate Abilities indicators
        for(int i = 0; i < indicator_ability.Length; i++)
        {
            if(i < abilities.indicator.Length)
            {
                if(abilities.indicator[i] != null)
                {
                    indicator_ability[i] = Instantiate(abilities.indicator[i]);
                    indicator_ability[i].transform.parent = gameObject.transform;
                    indicator_ability[i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {

        //Abilities indicators pos DONT NEED TO UPDATE WHEN DISABLED
        foreach (GameObject indicator in indicator_ability)
        {
            if(indicator != null)
            {
                indicator.transform.position = transform.position;
                //indicator_ability_1.transform.rotation = transform.rotation;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (abilityTrigger)
            {
                recastAbility(currentAbility);
                indicator_ability[currentAbility].SetActive(false);
                abilityTrigger = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //indicator
            if (abilities.cooldownTime <= 0)
            {
                if (abilities.quickCast)
                {
                    abilities.ability_1();
                }
                else
                {
                    currentAbility = 0;
                    indicator_ability[0].SetActive(true);
                    abilityTrigger = true;
                }
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

    void recastAbility(int num)
    {
        switch(num)
        {
            case 0:
                abilities.ability_1();
                break;
            case 1:
                abilities.ability_2();
                break;
            case 2:
                abilities.ability_3();
                break;
            case 3:
                abilities.ability_4();
                break;
        }
    }
}
