using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AbilitiesManager : NetworkBehaviour
{
    [SerializeField]
    public List<Ability> abilities = new List<Ability>();
    private int currentAbility;

    //private GameObject[] indicatorAbility;
    //public GameObject indicatorSpawnPosistion;

    private bool abilityTrigger = false;
    private Vector3 mouseLocation;
    public GameObject projectileSpawn;
    public GameObject projectileSpawnLocation;
    //public GameObject MouseWorldLocation;

    private Button[] abilityUIButtons;

    private float[] cooldownTime;
    private bool abilityCharge;
    private float chargeTime;
    private bool chargeTimeFire = false;

    public Slider chargeUISlider;
    PlayerMovement movement;
    public Color chargeStartColor;
    public Color chargeEndColor;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        movement = gameObject.GetComponent<PlayerMovement>();

        cooldownTime = new float[4];
        //indicatorAbility = new GameObject[numOfAbilities];

        //// Instantiate Abilities indicators
        //for (int i = 0; i < indicator_ability.Length; i++)
        //{
        //    if (i < abilities.indicators.Length)
        //    {
        //        if (abilities.indicators[i] != null)
        //        {
        //            indicator_ability[i] = Instantiate(abilities.indicators[i]);
        //            indicator_ability[i].transform.parent = gameObject.transform;
        //            indicator_ability[i].SetActive(false);
        //        }
        //    }
        //}

        // Buttons
        abilityUIButtons = new Button[5];

        for(int i = 0; i < 5; i++)
        {
            string btnName = "Button_" + i.ToString();
            abilityUIButtons[i] = GameObject.Find(btnName).GetComponent<Button>();

            abilityUIButtons[i].GetComponent<ButtonScript>().rechargeTime = abilities[i].cooldown;
            abilityUIButtons[i].GetComponent<ButtonScript>().abilityTitle = abilities[i].abilityTitle;
            abilityUIButtons[i].GetComponent<ButtonScript>().toolTip = abilities[i].toolTip;
            abilityUIButtons[i].GetComponent<ButtonScript>().abilityStatInfo = abilities[i].abilityStatInfo;
            abilityUIButtons[i].GetComponent<ButtonScript>().BtnSprite = abilities[i].uiSprite;
            abilityUIButtons[i].GetComponent<ButtonScript>().UpdateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // ProjectileSpawnLocation Rotation
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            mouseLocation = hit.point;
            Vector3 targetDir = mouseLocation - transform.position;
            float angle = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg;
            projectileSpawn.transform.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            //Debug.DrawLine(transform.position, hit.point);
        }

        // MouseWorldLocation.transform.position = mouseLocation;
        //indicator_posistion.transform.position = mouseLocation;
        //indicator_posistion.transform.rotation = abilities.projectilePoint.transform.rotation;

        ////Abilities indicators pos DONT NEED TO UPDATE WHEN DISABLED
        //foreach (GameObject indicator in indicator_ability)
        //{
        //    if (indicator != null)
        //    {
        //        indicator.transform.position = indicator_posistion.transform.position;
        //        indicator.transform.rotation = indicator_posistion.transform.rotation;

        //    }
        //}

        // Ability Cooldown
        for (int i = 0; i < cooldownTime.Length; i++)
        {
            if (cooldownTime[i] > 0)
            {
                cooldownTime[i] -= Time.deltaTime;
            }
        }

        // Charge Abilities
        if (abilityCharge == true)
        {
            if (chargeTime > 0)
            {
                chargeTime -= Time.deltaTime;
                chargeUISlider.value = 1 - (chargeTime / abilities[currentAbility].chargeTimer);
                //chargeUISlider.GetComponentInChildren<Image>().color = Color.Lerp(chargeStartColor, chargeEndColor, 1 - chargeUISlider.value);
                if (chargeUISlider.value > 0.95)
                {
                    chargeUISlider.GetComponentInChildren<Image>().color = chargeEndColor;
                }
            }
            else
            {
                //Delay
                StartCoroutine(Fade(chargeEndColor, 0, 0.2f));
                chargeTimeFire = true;
                abilityCharge = false;
                castAbility(currentAbility);
            }
        }

        if (Input.GetButtonDown("Ability1"))
        {
            // Indicator
            if (abilityTrigger)
            {
                castAbility(currentAbility);
                //indicatorAbility[currentAbility].SetActive(false);
                abilityTrigger = false;
            }
        }
        else if (Input.GetButton("Ability1"))
        {
            if (abilities.Count >= 1)
            {
                if (cooldownTime[0] <= 0 && abilityCharge == false)
                {
                    if (abilities[0].quickCast)
                        castAbility(0);
                    else
                    {
                        currentAbility = 0;
                        //indicatorAbility[0].SetActive(true);
                        abilityTrigger = true;
                    }
                    abilityCharge = true;
                    movement.slow(abilities[0].movementSlow, abilities[0].movementSlowDuration); 
                }
            }
        }
        else if (Input.GetButtonDown("Ability2"))
        {
            if(abilities.Count >= 2)
            {
                if (cooldownTime[1] <= 0 && abilityCharge == false)
                {
                    if (abilities[1].quickCast)
                         castAbility(1);
                    else
                    {
                        currentAbility = 1;
                        //indicatorAbility[0].SetActive(true);
                        abilityTrigger = true;
                    }
                    abilityCharge = true;
                    movement.slow(abilities[1].movementSlow, abilities[1].movementSlowDuration);
                }
            }
        }
        //else if (Input.GetButtonDown("Ability2"))
        //{
        //    if (abilities.Count >= 2)
        //    {
        //        if (abilities[1].cooldownTime <= 0)
        //        {
        //            if (abilities[1].quickCast)
        //                castAbility(1);
        //            else
        //            {
        //                currentAbility = 1;
        //                //indicatorAbility[0].SetActive(true);
        //                abilityTrigger = true;
        //            }
        //        }
        //    }
        //}
        //else if (Input.GetButtonDown("Ability3"))
        //{
        //    if (abilities.Count >= 3)
        //    {
        //        if (abilities[2].cooldownTime <= 0)
        //        {
        //            if (abilities[2].quickCast)
        //                castAbility(2);
        //            else
        //            {
        //                currentAbility = 2;
        //                //indicatorAbility[0].SetActive(true);
        //                abilityTrigger = true;
        //            }
        //        }
        //    }
        //}
        //else if (Input.GetButtonDown("Ability4"))
        //{
        //    if (abilities.Count >= 4)
        //    {
        //        if (abilities[3].cooldownTime <= 0)
        //        {
        //            if (abilities[3].quickCast)
        //                castAbility(3);
        //            else
        //            {
        //                currentAbility = 3;
        //                //indicatorAbility[0].SetActive(true);
        //                abilityTrigger = true;
        //            }
        //        }
        //    }
        //}
    }

    void castAbility(int num)
    {
        if (abilities[num].chargeTimer > 0 && chargeTimeFire == false) {
            
            chargeTime = abilities[num].chargeTimer;
            currentAbility = num;
            chargeUISlider.GetComponentInChildren<Image>().color = chargeStartColor;
            chargeUISlider.value = 0;
            //rechargeEnd = Time.time + abilities[num].chargeTimer;
            return;
        }
        
        if (abilityCharge == true)
            return;

        cooldownTime[num] = abilities[num].cooldown;
        Cmd_Cast(num);
        abilityUIButtons[num].onClick.Invoke();

        chargeTimeFire = false;
    }

    [Command]
    public void Cmd_Cast(int i)
    {

        abilities[i].Initilise(projectileSpawnLocation.transform);
        abilities[i].TriggerAbility();
    }

    public IEnumerator Fade(Color color, float end, float lerpTime)
    {
        yield return new WaitForSeconds(0.2f);
        float lerpStart = Time.time;
        float timeSinceStart = Time.time - lerpStart;
        float percentageComplete = timeSinceStart / lerpTime;
        float startA = color.a;

        while (true)
        {
            timeSinceStart = Time.time - lerpStart;
            percentageComplete = timeSinceStart / lerpTime;

            float currentValue = Mathf.Lerp(startA, end, percentageComplete);
            color.a = currentValue;
            chargeUISlider.GetComponentInChildren<Image>().color = color;

            if (percentageComplete >= 1)
                break;

            yield return new WaitForEndOfFrame();
        }
    }
}
