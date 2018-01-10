using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class Menu_Music : MonoBehaviour {
    public AudioClip Music;
    private AudioSource Main_Menu_Music;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Main_Menu_Music.clip = Music;
        Main_Menu_Music.Play();
    }
}
