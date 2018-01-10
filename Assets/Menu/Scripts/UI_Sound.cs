using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sound : MonoBehaviour
{
    //Todo Change it to Get of Tags
    public Slider Master;
    public Text MasterVolume;
    public Slider Gameplay;
    public Text GameplayVolume;
    public Slider Music;
    public Text MusicVolume;
    public Button Apply_button;
    //-----------------------------

    public List<AudioSource> Menu_Audio = new List<AudioSource>();
    public List<AudioSource> Game_Audio = new List<AudioSource>();
    private List<float> DefualtVolume = new List<float>();
    private List<float> DefualtVolume_Game = new List<float>();
    //Sort Master Value for in Game
    public float Master_Value;
    //Sort Gameplay Value for in Game
    public float Gameplay_Value;
    //Sort Music Value for in Game
    float Music_Value;
    //Tell Script to run in update as settings sound stay same. Only needs to apply
    public bool Game;
    public bool GameAudioAdd = true;

    //Scalers
    public int Scale;

    public float Temp = 100;
    // Use this for initialization
    void Start()
    {
        //Gets all AudioSource that in Main Menu
        Menu_Audio.AddRange(UnityEngine.GameObject.FindObjectsOfType<AudioSource>());
        //Makes a listener for Button
        Apply_button.onClick.AddListener(OnClick);


        if (!Game)
        {
            //run a loop get all Audio orignal Values Only Used on Main_Menu items as can be change multiple times
            for (int i = 0; i < Menu_Audio.Count; i++)
            {
                DefualtVolume.Add(Menu_Audio[i].volume);
            }
        }
    }
    private void Update()
    {
        //if (Game)
        //{
        //    if (GameAudioAdd)
        //    {
        //        //If in Game check for any new AudioSource 
                
        //        Game_Audio.AddRange(UnityEngine.GameObject.FindObjectsOfType<AudioSource>());
        //        GameAudioAdd = false;
        //        for (int i = 0; i < Game_Audio.Count; i++)
        //        {
        //            DefualtVolume_Game.Add(Game_Audio[i].volume);
        //        }
        //    }

        //    for (int i = 0; i < Game_Audio.Count; i++)
        //    {
        //        Game_Audio[i].volume = DefualtVolume_Game[i] * (Master_Value/Scale) * (Music_Value/Scale);
        //    }
        //}
        float MasterVolumeF = Scale * Master.value;
        float GameplayVolumeF = Scale * Gameplay.value;
        float MusicVolumeF = Scale * Music.value;
        MasterVolume.text = MasterVolumeF + "%";
        GameplayVolume.text = GameplayVolumeF + "%";
        MusicVolume.text = MusicVolumeF + "%";
    }
    void OnClick()
    {
        for (int i = 0; i < Menu_Audio.Count; i++)
        {
            Master_Value = Master.value;
            Music_Value = Music.value;
            Gameplay_Value = Gameplay.value;
            AudioListener.volume = DefualtVolume[i] * (Master_Value/ Scale) * (Music_Value/ Scale);

        }
    }
}
