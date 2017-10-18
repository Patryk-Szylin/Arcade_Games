using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AbilitiesManager : NetworkBehaviour
{

    [SerializeField]
    public Abilities abilities;

    public GameObject[] indicator_ability;
    public int currentAbility;
    private int numOfAbilities = 4;

    private bool abilityTrigger = false;

    // Use this for initialization
    void Start()
    {
        abilities = GetComponent<Abilities>();
        indicator_ability = new GameObject[numOfAbilities];

        // Instantiate Abilities indicators
        for (int i = 0; i < indicator_ability.Length; i++)
        {
            if (i < abilities.indicators.Length)
            {
                if (abilities.indicators[i] != null)
                {
                    indicator_ability[i] = Instantiate(abilities.indicators[i]);
                    indicator_ability[i].transform.parent = gameObject.transform;
                    indicator_ability[i].SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        //Abilities indicators pos DONT NEED TO UPDATE WHEN DISABLED
        foreach (GameObject indicator in indicator_ability)
        {
            if (indicator != null)
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

        if (Input.GetButtonDown("Ability1"))
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
        else if (Input.GetButtonDown("Ability2"))
        {
            //indicator
            if (abilities.cooldownTime_2 <= 0)
            {
                if (abilities.quickCast2)
                {
                    abilities.ability_2();
                }
                else
                {
                    currentAbility = 1;
                    indicator_ability[1].SetActive(true);
                    abilityTrigger = true;
                }
            }
        }
        else if (Input.GetButtonDown("Ability3"))
        {
            //indicator
            if (abilities.cooldownTime_3 <= 0)
            {
                if (abilities.quickCast3)
                {
                    abilities.ability_3();
                }
                else
                {
                    currentAbility = 2;
                    indicator_ability[2].SetActive(true);
                    abilityTrigger = true;
                }
            }
        }
        else if (Input.GetButtonDown("Ability4"))
        {
            abilities.ability_4();
        }
    }

    void recastAbility(int num)
    {
        switch (num)
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
