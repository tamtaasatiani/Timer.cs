using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private GameObject owner;
    private float time;
    private float interval;
    MonoBehaviourHook hook;

    private Action callback;

    
    private class MonoBehaviourHook : MonoBehaviour {
        public Action tick;

        private void Update()
        {
            if (tick != null) tick();
        }
    }

    

    public static void After(GameObject owner, float time, Action callback)
    {
        Timer timer = new Timer(owner, time, callback);
        owner.AddComponent<MonoBehaviourHook>().tick = timer.Tick;
        
    }

    public Timer(GameObject owner, float time, Action callback)
    {
        this.owner = owner;
        this.time = time;
        this.callback = callback;
    }

    private void Tick()
    {
        if (Complete())
        {
            callback();
            UnityEngine.Object.Destroy(owner.GetComponent<MonoBehaviourHook>());
            return;
        }

        time -= Time.deltaTime;
    }

    /*
    //This functionality isn't finished yet

    public void Every(float interval, Action callback)
    {
        this.interval = interval;
        Loop();
    }

    
    private void Loop()
    {
        Set(interval, callback);
        Update();

        if (Complete())
        {
            callback();
            this.time = this.interval;
        }

        Loop();
    }

    */

    public bool Complete()
    {
        return this.time <= 0f;
    }
}
