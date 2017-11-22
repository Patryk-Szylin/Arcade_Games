using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeed : MonoBehaviour {

    [SerializeField]
    GameObject killfeedPanelPrefab;

	// Use this for initialization
	void Start () {
        StartCoroutine("Init");
        //GameManager.Instance.onPlayerKilledCallback += OnKill;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(1f);

        GameManager.Instance.onPlayerKilledCallback += OnKill;
    }

    public void OnKill (string player, string source)
    {
        GameObject go = Instantiate(killfeedPanelPrefab, this.transform);
        //go.SetSiblingIndex(0);
        go.GetComponent<KillFeedPanel>().Setup(player, source);

        Destroy(go, 4f);
    }
}
