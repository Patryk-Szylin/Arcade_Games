using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion Effect", menuName = "Destructible Effects/Explosion")]
public class SO_Explode : SO_Destructible_Base
{
    public GameObject m_expplosionEffect;
    public float m_timeBeforeBuildingDestroy;

    public override IEnumerator DestroyedEffect(GameObject building)
    {
        var particles = m_expplosionEffect.GetComponent<ParticleSystem>();

        Instantiate(m_expplosionEffect, building.transform.position, building.transform.rotation);
        yield return new WaitForSeconds(m_timeBeforeBuildingDestroy);
        Destroy(building.gameObject);

    }
}
