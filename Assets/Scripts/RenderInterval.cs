using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderInterval : MonoBehaviour {
    [SerializeField] float interval = 0.5f;

    void Awake()
    {
        GetComponent<Camera>().enabled = false;
        InvokeRepeating("Render", 0, interval);
    }

    void Render()
    {
        GetComponent<Camera>().Render();
    }
}
