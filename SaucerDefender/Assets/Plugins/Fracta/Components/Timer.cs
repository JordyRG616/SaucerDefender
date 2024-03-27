using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Signals")]
    public Signal OnTimeStarted;
    public Signal<float> OnTimeTick;
    public Signal OnTimeOut;

    [Header("Unity Events")]
    public UnityEvent TimeStartedCallback;
    public UnityEvent<float> TimeTickCallback;
    public UnityEvent TimeOutCallback;

    public float time = 1;
    public bool countDown = false;
    public bool restartable;
    public bool resetOnTimeOut;
    public bool playOnAwake;
    private float counter;

    public bool Running { get; private set; }


    private void OnEnable()
    {
        if (playOnAwake) StartTimer();
    }

    public void StartTimer()
    {
        if (Running && !restartable) return;

        counter = countDown ? time : 0;
        OnTimeStarted.Fire();
        TimeStartedCallback?.Invoke();
        Running = true;
    }

    public void StopTimer()
    {
        Running = false;
        if (resetOnTimeOut)
        {
            counter = countDown ? time : 0;
            OnTimeTick.Fire(Mathf.Clamp01(counter / time));
            TimeTickCallback?.Invoke(Mathf.Clamp01(counter / time));
        }
    }

    private void Update()
    {
        if (Running)
        {
            if (!countDown) CountUp();
            else CountDown();
        }
    }

    private void CountUp()
    {
        counter += Time.deltaTime;
        OnTimeTick.Fire(Mathf.Clamp01(counter / time));
        TimeTickCallback?.Invoke(Mathf.Clamp01(counter / time));

        if (counter >= time)
        {
            Running = false;
            OnTimeOut.Fire();
            TimeOutCallback?.Invoke();

            if (resetOnTimeOut)
            {
                counter = 0;
                OnTimeTick.Fire(Mathf.Clamp01(counter / time));
                TimeTickCallback?.Invoke(Mathf.Clamp01(counter / time));
            }
        }
    }

    private void CountDown()
    {
        counter -= Time.deltaTime;
        OnTimeTick.Fire(Mathf.Clamp01(counter / time));
        TimeTickCallback?.Invoke(Mathf.Clamp01(counter / time));

        if (counter <= 0)
        {
            Running = false;
            OnTimeOut.Fire();
            TimeOutCallback?.Invoke();

            if (resetOnTimeOut)
            {
                counter = time;
                OnTimeTick.Fire(Mathf.Clamp01(counter / time));
                TimeTickCallback?.Invoke(Mathf.Clamp01(counter / time));
            }
        }
    }

    public void ChangeTime(float percentage)
    {
        time *= 1 + percentage;
    }

    public void SetTime(float newValue)
    {
        time = newValue;
    }
}
