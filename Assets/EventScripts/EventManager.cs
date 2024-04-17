using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager manager;

    public UnityEvent entryEvent;

    private List<Timer> timers = new();

    void Awake()
    {
        manager = this;
        AddTimer(3f, TimerDone);
    }

    void Update()
    {
        foreach (Timer t in timers) { t.Update(Time.deltaTime); }
        timers.RemoveAll(t => t.remove);
    }

    public void AddTimer(float time, Action callback) { timers.Add(new Timer(time, callback)); }

    public void TimerDone() { Debug.Log("done"); }
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
