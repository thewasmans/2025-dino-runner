using System;

public class Timer
{
    public event Action Timeout;
    public float TimeLeft { get; private set; }
    public float WaitTime { get; private set; } = 1.0f;
    public bool Started { get; private set; }
    public bool Finished { get; private set; }
    public bool Oneshot { get; private set; } = false;
    public bool Completed => Finished && TimeLeft <= 0f;

    public void StartTimer(float waitTime = -1)
    {
        WaitTime = waitTime < 0 ? WaitTime : waitTime;
        TimeLeft = waitTime;
        Started = true;
        Finished = false;
    }

    public void StopTimer()
    {
        Finished = true;
    }

    public void CancelTimer()
    {
        TimeLeft = 0;
        Started = false;
        Finished = false;
    }

    public void Progress(float deltaTime)
    {
        if (!Started || Finished) return;

        TimeLeft -= deltaTime;

        if (TimeLeft <= 0f)
        {
            TimeLeft = 0f;
            Finished = true;
            Started = false;
            Timeout?.Invoke();
            if (Oneshot)
                StartTimer(WaitTime);
        }
    }

    public float NormalizedProgress()
    {
        if (WaitTime <= 0f) return 0f;
        return 1f - (TimeLeft / WaitTime);
    }
}