﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

    private Animator animator;
    //public PressedButtonBehaviour pressedButtonBehaviour;
    public CooldownButtonBehaviour cooldownButtonBehaviour;

    public Slider rechargeSlider;
    public Image sliderFill;

    public float rechargeEnd;
    public float rechargeTime = 5.0f;

    public Color startColor;
    public Color endColor;

    private Text[] text;
    public Sprite BtnSprite;

    public Text toolTipText;
    public Text toolTipInfo;
    public Text toolTipStatInfo;
    private Image backgroundImage;
    public string abilityTitle;
    public string toolTip;
    public string abilityStatInfo;

    void Awake() {
        animator = GetComponent<Animator>();
        text = gameObject.GetComponentsInChildren<Text>();
    }

    // Use this for initialization
    void Start () {
        //pressedButtonBehaviour = animator.GetBehaviour<PressedButtonBehaviour>();
        //pressedButtonBehaviour.buttonScript = this;

        cooldownButtonBehaviour = animator.GetBehaviour<CooldownButtonBehaviour>();
        cooldownButtonBehaviour.buttonScript = this;

        backgroundImage = toolTipText.GetComponentInParent<Image>();
        //backgroundImage.enabled = false;
    }
	
    public void UpdateUI()
    {
        Image sprite = gameObject.GetComponent<Image>();
        sprite.overrideSprite = BtnSprite;
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
        if (rechargeSlider.value == 0.0f)
            animator.SetTrigger("Pressed");
    }

    public void keyHoveredEnter()
    {
        toolTipText.text = abilityTitle;
        toolTipInfo.text = toolTip;
        abilityStatInfo.Replace("\\n", "\n");
        toolTipStatInfo.text = abilityStatInfo;
        backgroundImage.enabled = true;
    }

    public void keyHoveredLeave()
    {
        backgroundImage.enabled = false;
        toolTipText.text = "";
        toolTipInfo.text = "";
        toolTipStatInfo.text = "";
    }

}
