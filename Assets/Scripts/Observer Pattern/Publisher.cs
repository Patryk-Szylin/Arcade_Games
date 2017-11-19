using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    ON_PLAYER_DEATH
}

public class Publisher : MonoBehaviour
{
    #region SINGLETON
    private static Publisher _instance;
    public static Publisher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Publisher>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("PublisherObject").AddComponent<Publisher>();
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

    public List<IObserver> m_observers = new List<IObserver>();


    public void Notify(EVENT_TYPE eventType)
    {
        for (int i = 0; i < m_observers.Count; i++)
        {
            m_observers[i].OnNotify(eventType);
        }
    }

    public void AddObserver(IObserver observer)
    {
        m_observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        m_observers.Remove(observer);
    }


}
