using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    

    public Dictionary<string, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if(!eventManager)
            {
                eventManager = FindObjectOfType( typeof(EventManager)) as EventManager;
                
                if(!eventManager)
                {
                    Debug.Log("There isnt exists an EventManager on a GameObject in your scene");
                }
                else
                {
                   eventManager.Initialize();
                }
            }
            return eventManager;
        }
    } 


    void Initialize()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))  //check if exists the eventName in this Dictionary
        {                                                             
            thisEvent.AddListener(listener);                                //and if exists put this listener(UnityAction) in this Event 
        }
        else
        {
            thisEvent = new UnityEvent();                                   //otherwise create New Event
            thisEvent.AddListener(listener);                                //Add the listener into this New Event
            instance.eventDictionary.Add(eventName, thisEvent);             //and Add the New Event plus the listener to EventDictionary
        }
    }
    
    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if(instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void EventTrigger(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            thisEvent.Invoke();
    }





}
