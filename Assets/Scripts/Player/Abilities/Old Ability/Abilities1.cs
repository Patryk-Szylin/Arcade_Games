using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Abilities1 : NetworkBehaviour
{
    [Space]
    public Button[] ability_Buttons;
    public GameObject[] indicators;
    public AudioClip[] abilitiesSound;
    [Space]
    public GameObject projectilePoint;
    public GameObject mouseLocation;

    [HideInInspector]
    public GameObject[] enemiesGO;

    [Space]
    public bool quickCast;
    public bool quickCast2;
    public bool quickCast3;
    public bool quickCast4;
    public float cooldown_1 = 4.0f;
    public float cooldown_2 = 4.0f;
    public float cooldown_3 = 4.0f;
    public float cooldown_4 = 4.0f;
    [HideInInspector] public float cooldownTime;
    [HideInInspector] public float cooldownTime_2; //turn into array
    [HideInInspector] public float cooldownTime_3;
    [HideInInspector] public float cooldownTime_4;

    public virtual void ability_1() { }
    public virtual void ability_2() { }
    public virtual void ability_3() { }
    public virtual void ability_4() { }
}
