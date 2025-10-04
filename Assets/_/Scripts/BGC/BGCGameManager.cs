using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BGCGameManager : BGCBaseManager<BGCGameData>
{
    [field: SerializeField] public BGCGameData GameData { get; private set; }
    [field: SerializeField] public BGCManager[] Managers { get; private set; }
    private Dictionary<Type, BGCManager> _managersInstances { get; set; } = new();

    public override void Initialize(BGCGameData gameData)
    {
        if (gameData)
            GameData = gameData;

        if (!GameData)
            throw new BGCException($"{nameof(BGCGameManager)} No {nameof(GameData)} refered. The {nameof(BGCGameManager)} need a {nameof(BGCGameData)}");

        _managersInstances = RetrieveInstancesManager();

        foreach (var manager in Managers)
        {
            manager.Initialize(this);
        }
    }

    protected Dictionary<Type, BGCManager> RetrieveInstancesManager()
    {
        Dictionary<Type, BGCManager> instances = new();
        foreach (var manager in Managers)
        {
            if (!instances.TryAdd(manager.GetType(), manager))
            {
                throw new BGCException($"[ {nameof(BGCGameManager)} ] The {manager.GetType()} is referenced multiple times in {Managers} when it should only be referenced once");
            }
        }
        return instances;
    }

    public T GetManager<T>() where T : BGCManager
    {
        if (_managersInstances.TryGetValue(typeof(T), out var manager))
        {
            return manager is T bgsManager ? bgsManager : default;
        }
        return default;
    }
}