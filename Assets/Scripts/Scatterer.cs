using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Scatterer : NetworkBehaviour
{
    [Header("Debug Specific")]
    public float m_gizmosRadius = 1f;

    [Header("Scatter Specific")]
    public int m_minOffset = -10;
    public int m_maxOffset = 10;

    public List<GameObject> m_itemVariation = new List<GameObject>();


    public override void OnStartServer()
    {
        base.OnStartServer();

        SpawnItems();
    }


    public void SpawnItems()
    {

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);
            Vector3 newChildPos;

            int randomOffsetX = Random.Range(m_minOffset, m_maxOffset);
            int randomOffsetZ = Random.Range(m_minOffset, m_maxOffset);

            newChildPos = new Vector3(child.position.x + randomOffsetX, child.position.y, child.position.z + randomOffsetZ);

            int randomIndex = Random.Range(0, m_itemVariation.Count);

            var randomItem = m_itemVariation[randomIndex];
            GameObject newItem = Instantiate(randomItem, newChildPos, Quaternion.identity) as GameObject;
            newItem.transform.parent = child.transform;
            NetworkServer.Spawn(newItem);


        }
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            var child = transform.GetChild(i);
            // TODO: Random offset added to the position of an item
            Gizmos.DrawWireSphere(child.position, m_gizmosRadius);
        }
        
    }


}
