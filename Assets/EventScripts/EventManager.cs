using SojaExiles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

//EventManager written by Vilmer Juvin
//This script creates and manages all the major and common events
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
    public UnityEvent masterBedroom;
    public UnityEvent winEvent;

    private bool commonEventOn;
    public List<Event> commonEvents;

    private List<Timer> timers = new();

    private float commonEventTimer = 60f;
    private float lastSanity;

    private GameObject left;
    private GameObject right;

    void Awake()
    {
        manager = this;
        commonEventOn = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        List<Transform> list = player.GetComponentsInChildren<Transform>().ToList();

        left = list.Find(x => x.name == "Left").gameObject;
        right = list.Find(x => x.name == "Right").gameObject;

        lastSanity = 100f;
    }

    void Update()
    {
        foreach (Timer t in timers) { t.Update(Time.deltaTime); }
        timers.RemoveAll(t => t.remove);

        if (!commonEventOn) { return; }

        float sanity = SanityManager.manager.Sanity;
        if (commonEventTimer <= 0)
        {
            CommonEvent(sanity);
            float equation = 3.1f * MathF.Pow(1.03f, 1.23f * sanity) - 3.1f;
            commonEventTimer = Math.Clamp(equation, 0.5f, float.PositiveInfinity);
            Debug.Log(commonEventTimer);
        }
        else if (commonEventTimer > 0)
        {
            commonEventTimer -= Time.deltaTime;
            float temp = lastSanity - sanity;
            if (temp > 0) { commonEventTimer -= temp; }
            lastSanity = sanity;
        }
    }

    public void CommonEventOn() { commonEventOn = true; }

    public void CommonEventTest() { CommonEvent(90f); }

    public void CommonEvent(float sanity)
    {
        //List<Event> events = commonEvents.FindAll(t => t.name == "MaleBreathSlow");
        List<Event> events = commonEvents.FindAll(t => t.sanity >= sanity);

        if (events.Count <= 0) { return; }

        Camera camera = Camera.main;
        System.Random rng = new System.Random();
        Event e;

        while (true)
        {
            if (events.Count <= 0) { return; }

            e = events[rng.Next(0, events.Count)];

            if (e.gameObject == null) { break; }

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

        e.unityEvent.Invoke();

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
    }

    public void PlaySoundFromRandomSide(string soundName)
    {
        if (soundName == null) { return; }
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        System.Random rng = new();
        float temp = rng.Next(0, 2); 
        if (temp == 0) { temp = -1; }
        Debug.Log(temp);

        GameObject ob;
        if (temp == 1) { ob = right; } else { ob = left; }
        AudioManager2.instance.Play(soundName, ob);
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
