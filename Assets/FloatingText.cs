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

    public void setText(string text)
    {
        damageText = animator.GetComponent<Text>();
        if (damageText != null)
            damageText.text = text;
    }
}
