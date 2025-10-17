using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnTimer : Manager
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public Transform ParentInstances { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
    [field: SerializeField] public Transform OutViewPoint { get; private set; }
    [field: SerializeField] public Timer Timer { get; private set; }
    private List<GameObject> _instances = new();
    public GameObject[] Instances => _instances.ToArray();

    public void Initialize(float WaitingTimeToSpawn)
    {
        Timer.Timeout += () =>
        {
            _instances.Add(InstantiatePrefab());
        };

        Timer.StartTimer(WaitingTimeToSpawn);
        GetManager<TimeManager>().TimeUpdated += deltaTime =>
        {
            Timer.Progress(deltaTime);
            UpdateInstances(deltaTime);
        };
    }
    public void DestroyInstance(GameObject instance)
    {
        _instances.Remove(instance);
        Destroy(instance);
    }

    public abstract GameObject InstantiatePrefab();

    public abstract void UpdateInstances(float deltaTime);
}
