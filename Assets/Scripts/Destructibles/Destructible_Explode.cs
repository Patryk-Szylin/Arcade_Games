using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Destructible Effects/Explosion")]
public class Destructible_Explode : Desctructible_Effect
{
    public GameObject m_explosionEffect;
    public float m_timeBeforeBuildingDestroy;

    public override IEnumerator DestroyedEffect(GameObject building)
    {
        Instantiate(m_explosionEffect, building.transform.position, building.transform.rotation);
        yield return new WaitForSeconds(m_timeBeforeBuildingDestroy);
        Destroy(building.gameObject);
    }
}
