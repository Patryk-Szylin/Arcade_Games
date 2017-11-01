using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class MyAbility : ScriptableObject
{
    public GameObject m_particleEffect;
    public AudioSource m_audioEffect;
    public bool m_isPassive = false;

    public abstract void ApplyTo(PlayerController pc);
    public abstract void ApplyToPlayer();

    public PlayerController m_player;

    // TODO: 
    // Some fancy effects when the ability is casted
}
