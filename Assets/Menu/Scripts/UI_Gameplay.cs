using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gameplay : MonoBehaviour {
    //Public Set for Objects in Settings -- ToDo find way to set these without habing drag then in
    public Text ResolutionLabel;
    public Text Quailty;
    public Toggle FullScreen;
    public Button Apply_button;
    //None public Values
    int width;
    int height;
    string[] minmax;
    // Use this for initialization
    void Start () {
        Apply_button.onClick.AddListener(OnClick);
    }
	
	// Update is called once per frame
	void Update () {
        //Set Resolution ---------------------------------------------------

        //Take Value from Resolution Dropdown split it
        string ResolutionT = ResolutionLabel.text;
        minmax = ResolutionT.Split('x');
        //Turn Values into Int's
        width = int.Parse(minmax[0]);
        height = int.Parse(minmax[1]);
        //int[] res = System.Array.ConvertAll(ResolutionT.Split('x'), new System.Converter<string, int>(int.Parse));

        //------------------------------------------------------------------

        

    }
    void OnClick()
    {
        //Set Resolution
        Screen.SetResolution(width, height, FullScreen.isOn);

        //Change Quailty Game Running at -- This is not very Nice want find better way
        if (Quailty.text == "Fastest")
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        if (Quailty.text == "Fast")
        {
            QualitySettings.SetQualityLevel(1, true);
        }
        if (Quailty.text == "Simple")
        {
            QualitySettings.SetQualityLevel(2, true);
        }
        if (Quailty.text == "Good")
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        if (Quailty.text == "Beautiful")
        {
            QualitySettings.SetQualityLevel(4, true);
        }
        if (Quailty.text == "Fantastic")
        {
            QualitySettings.SetQualityLevel(5, true);
        }

        //
    }
}
