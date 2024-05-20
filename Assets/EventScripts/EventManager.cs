using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//EventManager written by Vilmer Juvin
//This script handles the events
public class EventManager : MonoBehaviour
{
    public static EventManager manager;

    // To add a new event, create a new UnityEvent
    // Then add all subscribers in the editor
    // To trigger the event, access EventManager.manager.(eventName).Invoke() through any script
    public UnityEvent entryEvent;
    public UnityEvent mannequinEvent1;
    public UnityEvent girlEvent;
    public UnityEvent windowEvent;
    public UnityEvent tvEvent;
    public UnityEvent chessActivateEvent;
    public UnityEvent chessCompleteEvent;
    public UnityEvent masterBedroom;
    public UnityEvent winEvent;

    public List<Event> commonEvents;

    private List<Timer> timers = new();

    void Awake()
    {
        manager = this;
        AddTimer(10f, CommonEventTest);
    }

    void Update()
    {
        foreach (Timer t in timers) { t.Update(Time.deltaTime); }
        timers.RemoveAll(t => t.remove);
    }

    public void CommonEventTest() { CommonEvent(90f); }

    public void CommonEvent(float sanity) //This method randomly picks a common event from the list that meets certain criteria
    {
        List<Event> events = commonEvents.FindAll(t => t.sanity >= sanity); //Criteria 1: the player has low enough sanity

        if (events.Count <= 0) { return; }

        Camera camera = Camera.main;
        System.Random rng = new System.Random();
        Event e;

        while (true)
        {
            if (events.Count <= 0) { return; }

            e = events[rng.Next(0, events.Count)];

            if (IsVisible(camera, e.gameObject)) //Criteria 2: checks if the camera sees the selected event, if not, picks new event
            {
                events.Remove(e);
                continue;
            }
            else
            {
                break;
            }
        }

        bool IsVisible(Camera c, GameObject target)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(c);
            var point = target.transform.position;

            foreach (var plane in planes)
            {
                if (plane.GetDistanceToPoint(point) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        e.unityEvent.Invoke();
    }

    public void AddTimer(float time, Action callback) { timers.Add(new Timer(time, callback)); }
}

[Serializable]
public struct Event
{
    public string name;
    public float sanity;
    public GameObject gameObject;
    public UnityEvent unityEvent;

    public Event(string name,  float sanity, GameObject gameObject, UnityEvent unityEvent)
    {
        this.name = name;
        this.sanity = sanity;
        this.gameObject = gameObject;
        this.unityEvent = unityEvent;
    }
}

public class Timer
{
    private float timer;
    private Action callback;
    public bool remove;

    public Timer(float time, Action callback)
    {
        timer = time;
        this.callback = callback;
        remove = false;
    }

    public void Update(float deltaTime)
    {
        timer -= deltaTime;
        if (timer <= 0)
        {
            callback();
            remove = true;
        }
    }
}
