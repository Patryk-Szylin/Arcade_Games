using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MapObject : NetworkBehaviour
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class MiniMapController : MonoBehaviour {

    public Transform playerPos;
    public Camera mapCamera;
    public float minimapDrawDistance = 35;

    public static List<MapObject> mapObjects = new List<MapObject>();

    void Start()
    {
        for(int i=0; i < GameManager.m_allPlayers[i].Count;i++)
        {
            if(GameManager.m_allPlayers[i].isLocalPlayer)
            {
                playerPos = GameManager.m_allPlayers[i].transform.position;
            }
        }
    }

    public static void RegisterMapObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        mapObjects.Add(new MapObject() { owner = o, icon = image });
    }

    public static void RemoveMapObject(GameObject o)
    {
        List<MapObject> newList = new List<MapObject>();
        for (int i = 0; i < mapObjects.Count; i++)
        {
            if(mapObjects[i].owner == o)
            {
                Destroy(mapObjects[i].icon);
                continue;
            }
            else
            {
                newList.Add(mapObjects[i]);
            }
        }

        mapObjects.RemoveRange(0, mapObjects.Count);
        mapObjects.AddRange(newList);
    }

    void DrawMapIcons()
    {
        foreach(MapObject mo in mapObjects)
        {
            Vector2 mop = new Vector2(mo.owner.transform.position.x, mo.owner.transform.position.z);
            Vector2 pp = new Vector2(playerPos.position.x, playerPos.position.z);

            if (Vector2.Distance(mop, pp) > minimapDrawDistance)
            {
                mo.icon.enabled = false;
                continue;
            }
            else
            {
                mo.icon.enabled = true;
            }

            Vector3 screenPos = mapCamera.WorldToViewportPoint(mo.owner.transform.position);
            mo.icon.transform.SetParent(this.transform);

            RectTransform rt = this.GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);


            screenPos.x = Mathf.Clamp(screenPos.x * rt.rect.width + corners[0].x, corners[0].x, corners[2].x);
            screenPos.y = Mathf.Clamp(screenPos.y * rt.rect.height + corners[0].y, corners[0].y, corners[1].y);

            //screenPos.x = screenPos.x * rt.rect.width + corners[0].x;
            //screenPos.y = screenPos.y * rt.rect.height + corners[0].y;

            screenPos.z = 0;
            mo.icon.transform.position = screenPos;
        }
    }

	
	// Update is called once per frame
	void Update ()
    {
        DrawMapIcons();
	}
}
