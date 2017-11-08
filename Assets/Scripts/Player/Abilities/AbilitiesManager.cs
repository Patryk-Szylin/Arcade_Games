using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AbilitiesManager : NetworkBehaviour
{
    [SerializeField]
    public Abilities abilities;

    private GameObject[] indicator_ability;
    public GameObject indicator_posistion;
    private int currentAbility;
    private int numOfAbilities = 4;

    private bool abilityTrigger = false;
    private Vector3 mouseLocation;
    public GameObject target;

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

        // Find Buttons
        abilities.ability_Buttons = new Button[numOfAbilities];
        abilities.ability_Buttons[0] = GameObject.Find("Button_1").GetComponent<Button>();
        abilities.ability_Buttons[1] = GameObject.Find("Button_2").GetComponent<Button>();
        abilities.ability_Buttons[2] = GameObject.Find("Button_3").GetComponent<Button>();
        abilities.ability_Buttons[3] = GameObject.Find("Button_4").GetComponent<Button>();

        abilities.ability_Buttons[0].GetComponent<ButtonScript>().rechargeTime = abilities.cooldown_1;
        abilities.ability_Buttons[1].GetComponent<ButtonScript>().rechargeTime = abilities.cooldown_2;
        abilities.ability_Buttons[2].GetComponent<ButtonScript>().rechargeTime = abilities.cooldown_3;
        abilities.ability_Buttons[3].GetComponent<ButtonScript>().rechargeTime = abilities.cooldown_4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //TO BE REFACTORED
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            mouseLocation = hit.point;
            Vector3 targetDir = mouseLocation - transform.position;
            float angle = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg;
            abilities.projectilePoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            Debug.DrawLine(transform.position, hit.point);
        }

        target.transform.position = mouseLocation;
        indicator_posistion.transform.position = mouseLocation;
        indicator_posistion.transform.rotation = abilities.projectilePoint.transform.rotation;

        //Abilities indicators pos DONT NEED TO UPDATE WHEN DISABLED
        foreach (GameObject indicator in indicator_ability)
        {
            if (indicator != null)
            {
                indicator.transform.position = indicator_posistion.transform.position;
                indicator.transform.rotation = indicator_posistion.transform.rotation;

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
            // indicator
            if (abilities.cooldownTime <= 0)
            {
                if (abilities.quickCast)
                    recastAbility(0);
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
                    recastAbility(1);
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
                    recastAbility(2);
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
            //indicator
            if (abilities.cooldownTime_4 <= 0)
            {
                if (abilities.quickCast4)
                    recastAbility(3);
                else
                {
                    currentAbility = 3;
                    indicator_ability[4].SetActive(true);
                    abilityTrigger = true;
                }
            }
        }
    }

    void recastAbility(int num)
    {
        switch (num)
        {
            case 0:
                abilities.ability_1();
                abilities.ability_Buttons[0].onClick.Invoke();
                break;
            case 1:
                abilities.ability_2();
                abilities.ability_Buttons[1].onClick.Invoke();
                break;
            case 2:
                abilities.ability_3();
                abilities.ability_Buttons[2].onClick.Invoke();
                break;
            case 3:
                abilities.ability_4();
                abilities.ability_Buttons[3].onClick.Invoke();
                break;
        }
    }
}
