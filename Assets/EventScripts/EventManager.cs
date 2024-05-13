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
    // To trigger the event, access EventManager.manager.(eventName).Invoke() through any script
    public UnityEvent entryEvent;
    public UnityEvent mannequinEvent1;
    public UnityEvent tvEvent;
    public UnityEvent chessActivateEvent;
    public UnityEvent chessCompleteEvent;

    public List<Event> commonEvents;

    private List<Timer> timers = new();

    void Awake()
    {
        manager = this;
    }

    void Update()
    {
        foreach (Timer t in timers) { t.Update(Time.deltaTime); }
        timers.RemoveAll(t => t.remove);
    }

    public void CommonEvent(float sanity)
    {
        List<Event> events = commonEvents.FindAll(t => t.sanity >= sanity);

        Camera camera = Camera.main;
        System.Random rng = new System.Random();
        Event e;

        while (true)
        {
            if (events.Count <= 0) { return; }

            e = events[rng.Next(0, events.Count)];

            Vector3 vpPos = camera.WorldToViewportPoint(e.transform.position);
            if (vpPos.x >= 0f && vpPos.x <= 1f && vpPos.y >= 0f && vpPos.y <= 1f && vpPos.z > 0f)
            {
                events.Remove(e);
                continue;
            }
            else
            {
                break;
            }
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
    public Transform transform;
    public UnityEvent unityEvent;

    public Event(string name,  float sanity, Transform transform, UnityEvent unityEvent)
    {
        this.name = name;
        this.sanity = sanity;
        this.transform = transform;
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
