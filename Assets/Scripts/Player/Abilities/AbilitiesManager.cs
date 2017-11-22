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

        cooldownTime = new float[5];

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
        }

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
            casta(0);
        }
        else if (Input.GetButton("Ability1"))
        {
            casta(0);
        }
        else if (Input.GetButtonDown("Ability2"))
        {
            casta(1);
        }
        else if (Input.GetButtonDown("Ability3"))
        {
            casta(2);
        }
        else if (Input.GetButtonDown("Ability4"))
        {
            casta(3);
        }
        else if (Input.GetButtonDown("Ability5"))
        {
            casta(4);
        }
    }

    void castAbility(int num)
    {
        if (abilities[num].chargeTimer > 0 && chargeTimeFire == false) {
            chargeTime = abilities[num].chargeTimer;
            currentAbility = num;
            chargeUISlider.GetComponentInChildren<Image>().color = chargeStartColor;
            chargeUISlider.value = 0;
            return;
        }
        
        if (abilityCharge == true)
            return;


        var dir = GetAbilityPointInWorldSpace();

        cooldownTime[num] = abilities[num].cooldown;
        Cmd_Cast(num, dir);
        abilityUIButtons[num].onClick.Invoke();

        chargeTimeFire = false;
    }

    [Command]
    public void Cmd_Cast(int i, Vector3 direction)
    {
        if (abilities[i].burst == true)
        {
            abilities[i].numOfShotsDone = 0;
            StartCoroutine(burstFire(i, direction));
            //Debug.Log(abilities[i].numOfShotsDone);
        }
        else
        {
            abilities[i].Initilise(projectileSpawnLocation.transform, transform.name, direction);
            abilities[i].TriggerAbility();
        }
    }

    public IEnumerator burstFire(int index, Vector3 direction)
    {
        abilities[index].numOfShotsDone += 1;
        // Fire
        abilities[index].Initilise(projectileSpawnLocation.transform, transform.name, direction);
        abilities[index].TriggerAbility();

        yield return new WaitForSeconds(abilities[index].timeBetweenShots);

        if (abilities[index].numOfShotsDone < abilities[index].numOfShots)
        {
            StartCoroutine(burstFire(index, direction));
        }
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

    void casta(int i)
    {
        if (cooldownTime[i] <= 0 && abilityCharge == false)
        {
            if (abilities[i].quickCast)
                castAbility(i);
            else
            {
                currentAbility = i;
                abilityTrigger = true;
            }

            abilityCharge = true;
            movement.slow(abilities[i].movementSlow, abilities[i].movementSlowDuration);
        }
    }

    public void updateUI(int i)
    {
        if (i > 4)
            return;

        string btnName = "Button_" + i.ToString();
        abilityUIButtons[i] = GameObject.Find(btnName).GetComponent<Button>();

        abilityUIButtons[i].GetComponent<ButtonScript>().rechargeTime = abilities[i].cooldown;
        abilityUIButtons[i].GetComponent<ButtonScript>().abilityTitle = abilities[i].abilityTitle;
        abilityUIButtons[i].GetComponent<ButtonScript>().toolTip = abilities[i].toolTip;
        abilityUIButtons[i].GetComponent<ButtonScript>().abilityStatInfo = abilities[i].abilityStatInfo;
        abilityUIButtons[i].GetComponent<ButtonScript>().BtnSprite = abilities[i].uiSprite;
        abilityUIButtons[i].GetComponent<ButtonScript>().UpdateUI();
    }


    public Vector3 GetAbilityPointInWorldSpace()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //var rayWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, 99999f))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
