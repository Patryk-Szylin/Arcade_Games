using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GAME_EVENT
{
    PLAYER_DEATH,
    PLAYER_WIN,

    // Projectile related
    DOT_HIT
}



public class Publisher : MonoBehaviour
{   

    //private static Dictionary<GAME_EVENT, Dictionary<GameObject, Action>> m_listeners = new Dictionary<GAME_EVENT, Dictionary<GameObject, Action>>();
    private Dictionary<GAME_EVENT, List<Action>> m_listeners = new Dictionary<GAME_EVENT, List<Action>>();

    public static Publisher Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void Add(GAME_EVENT Event, Action callback)
    {
        if (!m_listeners.ContainsKey(Event))
        {
            m_listeners.Add(Event, new List<Action>());
        }

        m_listeners[Event].Add(callback);
    }


    public void PostNotification(GAME_EVENT gameEvent)
    {
        if (!m_listeners.ContainsKey(gameEvent))
            return;


        foreach (var entry in m_listeners[gameEvent])
        {
            entry.Invoke();
        }

        //foreach(KeyValuePair<GameObject, Action> entry in m_listeners[gameEvent])
        //{
        //    entry.Value.Invoke();
        //}
    }



}
