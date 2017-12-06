using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Explosion and Burning Effect", menuName = "Destructible Effects/Explosion and Burning")]
public class SO_ExplosionWithBurningEffect : SO_Destructible_Base
{
    public GameObject m_explosionFX;
    public GameObject m_burningFX;
    public float m_timeBeforeBuildingDestroy;

    public override IEnumerator DestroyedEffect(GameObject building)
    {
        var particles = m_explosionFX.GetComponent<ParticleSystem>();

        var explosionFX = Instantiate(m_explosionFX, building.transform.position, building.transform.rotation);
        NetworkServer.Spawn(explosionFX);
        yield return new WaitForSeconds(m_timeBeforeBuildingDestroy);
        var burnFX = Instantiate(m_burningFX, building.transform.position + new Vector3(-1, 0, -1), m_burningFX.transform.rotation);
        NetworkServer.Spawn(burnFX);

    }

}
