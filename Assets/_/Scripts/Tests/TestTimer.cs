using UnityEngine;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

public class TestTimer
{
    TimeManager TimeManager;

    static float[] TimerValues = new[] { 1, 2, 3.5f, 4 };

    [SetUp]
    public void Setup()
    {
        TimeManager = new GameObject(nameof(TimeManager)).AddComponent<TimeManager>();
        TimeManager.Initialize();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(TimeManager.gameObject);
    }

    [UnityTest]
    public IEnumerator TestStartTimerOverTime([ValueSource(nameof(TimerValues))] float timerValue)
    {
        Timer timer = new Timer();
        TimeManager.TimeUpdated += timer.Progress;
        timer.StartTimer(timerValue);
        yield return new WaitForSeconds(timerValue / 2);

        Assert.True(timer.Started, $"The {nameof(timer)} should be Started");

        yield return new WaitForSeconds(timerValue / 2);

        Assert.AreEqual(timerValue, timer.WaitTime, $"The {nameof(Timer.WaitTime)} from the {nameof(timer)} expected is {timerValue}");
        Assert.True(timer.Finished, $"The {nameof(Timer)} should be finished");
        Assert.Zero(timer.TimeLeft, $"The {nameof(Timer.TimeLeft)} from the {nameof(timer)} should be at zero");
    }

    [UnityTest]
    public IEnumerator TestCancelTimerOverTime([ValueSource(nameof(TimerValues))] float timerValue)
    {
        Timer timer = new Timer();
        TimeManager.TimeUpdated += timer.Progress;
        timer.StartTimer(timerValue);
        yield return new WaitForSeconds(timerValue / 2);
        timer.CancelTimer();
        yield return new WaitForSeconds(timerValue / 2);

        Assert.False(timer.Finished, $"The {nameof(timer)} should be not finished");
        Assert.False(timer.Started, $"The {nameof(timer)} should be not started");
        Assert.Zero(timer.TimeLeft, $"The {nameof(Timer.TimeLeft)} from the {nameof(timer)} should be at zero");
    }
}
