using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {

    public Animator animator;
    public Text damageText;

	// Use this for initialization
	void Start () {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        damageText = animator.GetComponent<Text>();
    }

    public void setText(float text, bool heal)
    {
        damageText = animator.GetComponent<Text>();
        if(text > 25)
        {
            animator.Play("UI_PopUpTextBold_Anim");
            damageText.fontStyle = FontStyle.Bold;
        }

        if(heal == true)
        {
            //set text to green
        }

        if (damageText != null)
            damageText.text = text.ToString();
    }
}
