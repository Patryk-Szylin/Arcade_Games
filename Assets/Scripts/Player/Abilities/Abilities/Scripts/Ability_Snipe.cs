using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Snipe", fileName = "Snipe")]
public class Ability_Snipe : AbilityProjectile
{
    public Projectile launcher;
    //[HideInInspector]
    [Header("Snipe Variables")]
    public float damage;
    public float range;
    public float blastRadius;

    public bool burstFire = false;
    public int numOfShots;
    [HideInInspector] public int numOfShotsDone;
    public float timeBetweenShots;
    public HelperFuctions helper;
    GameObject PrefabGameObjectWithExampleMonoBehaviour;

    public override void Initilise(Transform projectileSpawnLocation, string sourceID)
    {
        Vector3 direction = GetAbilityPointInWorldSpace();
        launcher = projectilePrefab.GetComponent<Projectile>();

        // Projectile Stats
        launcher.damage = damage;
        launcher.range = range;
        launcher.projectileForce = projectileForce;
        launcher.blastRadius = blastRadius;

        // ----
        launcher.m_prefab = projectilePrefab;
        launcher.projectileSpawnLocation = projectileSpawnLocation;
        launcher.m_impactFX = m_impactFX;
        launcher.sourceID = sourceID;
    }

    public override void TriggerAbility()
    {
        if (burstFire == true)
        {
            numOfShotsDone = 0;
            //HelperFuctions.instance.StartCoroutine(burst1());
            //helper.StartCoroutine(burst1());
            //StartCoroutine(burst());
        }
        else
        {
            launcher.Launch();
        }

    }

    //public IEnumerator burst()
    //{
    //    numOfShotsDone += 1;
    //    Debug.Log(numOfShotsDone);
    //    launcher.Launch();
    //    yield return new WaitForSeconds(timeBetweenShots);
    //    float a = Time.time;

    //    if (numOfShotsDone < numOfShots)
    //    {
    //        burst();
    //    }
    //}
}
