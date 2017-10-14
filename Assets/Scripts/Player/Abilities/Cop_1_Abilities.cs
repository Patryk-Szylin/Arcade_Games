using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop_1_Abilities : MonoBehaviour {

    [Header("Taser Variables")]
    [Space]
    public bool quickCast;
    public GameObject indicator;
    [Range(0, 10)]
    [SerializeField]
    public float cooldown;
    private float cooldownTime;
    [Range(0, 10)]
    public float range;
    [Range(0, 10)]
    public float castTime;
    private bool castDelayed = false;
    private float delayTime;
    [Range(0, 10)]
    public float stunDuration;

    [Header("Flash Bang Variables")]
    [Space]
    public bool quickCast1;
    public Sprite indicator1;
    [Range(0, 10)]
    [SerializeField]
    private float cooldown1;
    [Range(0, 10)]
    public float range1;
    [Range(0, 10)]
    public float castTime1;
    [Range(0, 10)]
    public float stunDuration1;

    [Header("Pepper Spray Variables")]
    [Space]
    [Range(0, 9)]
    public float Cooldown_A3;
    [Range(0, 9)]
    public float ChannelDur_A3;
    [Range(0, 9)]
    public float Speed_A3;

    [Header("Ability 4 Variables")]
    [Space]
    [Range(0, 9)]
    public float Cooldown2;
    [Range(0, 9)]
    public float ChannelDur_A2;
    [Range(0, 9)]
    public float Speed_A2;
    [Range(0, 9)]
    public float Projectile_Speed_A2;

    

    [System.Serializable]
    public struct bulletData
    {
        public float Speed_A2555;
        public int Cooldown5;
    }

    public bulletData equippedItem;

    void Update()
    {
        // -----Taser-----
        if (cooldownTime > 0 || cooldown == 0)
        { 
            cooldownTime -= Time.deltaTime;
            delayTime -= Time.deltaTime;

            // Cast Time
            if (delayTime < 0 && castDelayed == true)
                ability_1();
        }
    }

    // -----Taser-----
    public void ability_1()
    {
        // Cooldown
        if (cooldownTime <= 0 && !castDelayed)
        {
            castDelayed = true;
            Debug.Log("Cast Delayed");
            delayTime = castTime;
            cooldownTime = cooldown;
        }
        else if (castDelayed && delayTime < 0)
        {
            castDelayed = false;
            Debug.Log("Fire");

            // Raycast
            RaycastHit hit;
            Vector3 direction = transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (hit.transform.tag == "Target")
                {
                    Debug.Log("hit!");
                    //hit.collider.gameObject.GetComponent<Movement>().stun(stunDuration);
                }
            }
        }
    }

    public void ability_2() {
        print("2");
    }
    public void ability_3() {
        print("3");
    }
    public void ability_4() {
        print("4");
    }

}
