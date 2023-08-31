using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(this);
    }

    Dictionary<EVENT_TYPE, List<IListener>> Listeners = new Dictionary<EVENT_TYPE, List<IListener>>();

    //리스너+
    public void AddListener(EVENT_TYPE Event_Type, IListener Listener)
    {
        List<IListener> ListenList = null;

        if (Listeners.TryGetValue(Event_Type, out ListenList))
        {
            ListenList.Add(Listener);
            return;
        }
        ListenList = new List<IListener>();
        ListenList.Add(Listener);
        Listeners.Add(Event_Type, ListenList);
    }


    // 이벤트 -> 리스너
    public void PostNotification(EVENT_TYPE Event_Type, Component Sender, Object Param = null)
    {


        List<IListener> ListenList = null;

        if (!Listeners.TryGetValue(Event_Type, out ListenList))
            return;

        for (int i = 0; i < ListenList.Count; i++)
        {
            if (!ListenList[i].Equals(null))
                ListenList[i].OnEvent(Event_Type, Sender, Param);
        }
    }

    //리스너 -
    public void RemoveEvent(EVENT_TYPE Event_Type)
    {
        Listeners.Remove(Event_Type);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<IListener>> TmpListeners = new Dictionary<EVENT_TYPE, List<IListener>>();

        foreach (KeyValuePair<EVENT_TYPE, List<IListener>> Item in Listeners)
        {
            for (int i = Item.Value.Count - 1; i >= 0; i--)
            {
                if (Item.Value[i].Equals(null))
                    Item.Value.RemoveAt(i);
            }
        }
        Listeners = TmpListeners;
    }

    private void OnLevelWasLoaded(int level)
    {
        RemoveRedundancies();
    }
}

public enum EVENT_TYPE
{
    ENone = -1
}

public interface IListener
{
    void OnEvent(EVENT_TYPE eventType,Component sender, object param = null);
}
