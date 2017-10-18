using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Abilities : NetworkBehaviour
{
    public GameObject[] indicators;
    public AudioClip[] abilitiesSound;

    public GameObject projectilePoint;

    public GameObject[] enemiesGO;

    public bool quickCast;
    public bool quickCast2;
    public bool quickCast3;
    public float cooldownTime;
    public float cooldownTime_2; //turn into array
    public float cooldownTime_3;

    public virtual void ability_1() { }
    public virtual void ability_2() { }
    public virtual void ability_3() { }
    public virtual void ability_4() { }
}
