using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFuctions : MonoBehaviour {
    public static HelperFuctions instance;

    void Start()
    {
        HelperFuctions.instance = this;
    }

    public IEnumerator burst1()
    {
        Debug.Log("DEASF");
        yield return new WaitForSeconds(0.4f);
        Debug.Log("DEA234344SF");
    }
}


