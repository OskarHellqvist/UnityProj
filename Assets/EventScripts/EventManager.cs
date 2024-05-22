using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager manager;

    // To add a new event, create a new UnityEvent
    // Then add all subscribers in the editor
    // To trigger the event, access EventManager.playerInstance.(eventName).Invoke() through any script
    public UnityEvent entryEvent;
    public UnityEvent mannequinEvent1;
    public UnityEvent tvEvent;
    public UnityEvent chessActivateEvent;
    public UnityEvent chessCompleteEvent;
    public UnityEvent masterBedroom;
    public UnityEvent winEvent;
    public UnityEvent handEvent;

    public List<Event> commonEvents;

    private List<Timer> timers = new();

    private float commonEventTimer = 20f;

    void Awake()
    {
        manager = this;
        AddTimer(10f, CommonEventTest);
    }

    void Update()
    {
        foreach (Timer t in timers) { t.Update(Time.deltaTime); }
        timers.RemoveAll(t => t.remove);

        if (commonEventTimer <= 0)
        {
            float sanity = SanityManager.playerInstance.Sanity;
            CommonEvent(sanity);
            commonEventTimer = sanity / 5;
        }
        else if (commonEventTimer > 0)
        {
            commonEventTimer -= Time.deltaTime;
        }
    }

    public void CommonEventTest() { CommonEvent(90f); }

    public void CommonEvent(float sanity)
    {
        List<Event> events = commonEvents.FindAll(t => t.sanity >= sanity);

        if (events.Count <= 0) { return; }

        Camera camera = Camera.main;
        System.Random rng = new System.Random();
        Event e;

        while (true)
        {
            if (events.Count <= 0) { return; }

            e = events[rng.Next(0, events.Count)];

            if (IsVisible(camera, e.gameObject))
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
