using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    private Animator animator;
    private PressedButtonBehaviour pressedButtonBehaviour;
    private CooldownButtonBehaviour cooldownButtonBehaviour;

    public Slider rechargeSlider;
    public Image sliderFill;

    public float rechargeEnd;
    public float rechargeTime = 5.0f;

    public Color startColor;
    public Color endColor;

    private Text[] text;

    void Awake() {
        animator = GetComponent<Animator>();
        text = gameObject.GetComponentsInChildren<Text>();
    }

    // Use this for initialization
    void Start () {
        pressedButtonBehaviour = animator.GetBehaviour<PressedButtonBehaviour>();
        pressedButtonBehaviour.buttonScript = this;

        cooldownButtonBehaviour = animator.GetBehaviour<CooldownButtonBehaviour>();
        cooldownButtonBehaviour.buttonScript = this;

        //text[0].text = Input.GetButton();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartRecharge()
    {
        rechargeEnd = Time.time + rechargeTime;
        rechargeSlider.value = 1.0f;
    }

    public void SetOverlay()
    {
        rechargeSlider.value = (rechargeEnd - Time.time) / rechargeTime;
        sliderFill.color = Color.Lerp(startColor, endColor, 1 - rechargeSlider.value);
        float textValue = Mathf.Round(rechargeEnd - Time.time);
        if(textValue < 1)
        {
            textValue = (rechargeEnd - Time.time);
            text[1].text = textValue.ToString("0.0");
        }
        else
        {
            text[1].text = textValue.ToString();
        }
    }

    public void EndRecharge()
    {
        rechargeSlider.value = 0.0f;
        text[1].text = "";
    }

    public void KeyPressed()
    {
        if(rechargeSlider.value == 0.0f)
            animator.SetTrigger("Pressed");
    }
}
