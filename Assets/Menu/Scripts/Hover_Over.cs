using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover_Over : MonoBehaviour {

    // Use this for initialization
    //When the mouse hovers over the GameObject, it turns to this color (red)
    Color m_MouseOverColor = Color.red;
    //This stores the GameObject’s original color
    Color m_OriginalColor;
    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    Image m_Renderer;

    void Start()
    {
        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<Image>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.color;
    }

    void OnMouseOver()
    {
        //Change the color of the GameObject to red when the mouse is over GameObject
        m_Renderer.color = m_MouseOverColor;
    }

    void OnMouseExit()
    {
        //Reset the color of the GameObject back to normal
        m_Renderer.color = m_OriginalColor;
    }
}
