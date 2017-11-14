using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    #region SINGLETON
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion


    public List<Text> m_cooldownDisplayTexts;
    public List<Image> m_darkMasks;
    public List<Image> m_abilitySprites;
    public List<Text> m_abilityTooltipObjects;
    public List<Text> m_abilityToolcontent;
    


    public void DisplayToolTip(int abilityIndex)
    {
        //m_abilityTooltipObjects[abilityIndex].text = m_abilityToolcontent[abilityIndex].text;
        m_abilityTooltipObjects[abilityIndex].gameObject.SetActive(true);
        
    }

    public void HideToolTip(int abilityIndex)
    {
        m_abilityTooltipObjects[abilityIndex].gameObject.SetActive(false);
    }



}
