using System;
using UnityEngine;

public class TimeManager : Manager
{
    public event Action<float> TimeUpdated;
    public float CurrentTime { get; private set; }
    public bool Progressing { get; private set; }
    [field: SerializeField] public float TimeScale { get; private set; } = 1f;

    public override void Initialize()
    {
        StartTime();
    }

    public void StartTime()
    {
        CurrentTime = 0;
        Progressing = true;
    }

    public void Pause()
    {
        Progressing = false;
    }

    public void UnPause()
    {
        Progressing = true;
    }

    void Update()
    {
        Time.timeScale = TimeScale;
        if (Progressing)
        {
            CurrentTime += Time.deltaTime;
            TimeUpdated?.Invoke(Time.deltaTime);
        }
    }
}
