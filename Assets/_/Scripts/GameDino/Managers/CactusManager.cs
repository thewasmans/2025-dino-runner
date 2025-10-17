using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CactusManager : Manager
{
    [field: SerializeField] public GameObject Prefab_Cactus { get; private set; }
    [field: SerializeField] public Transform ParentCactus { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
    [field: SerializeField] public Transform OutViewPoint { get; private set; }
    [field: SerializeField] public Timer Timer { get; private set; }
    public List<GameObject> Cactus = new();

    public override void Initialize()
    {
        Timer.Timeout += () =>
        {
            SpawnCactus();
        };
        Timer.StartTimer(GameData.WaitingTimeToSpawnCactus);
        GetManager<TimeManager>().TimeUpdated += deltaTime =>
        {
            Timer.Progress(deltaTime);
            UpdateCactus(deltaTime);
        };
    }

    [Button]
    public void SpawnCactus()
    {
        Cactus.Add(Instantiate(Prefab_Cactus, SpawnPoint.position, Quaternion.identity, ParentCactus));
    }

    public void UpdateCactus(float deltatime)
    {
        foreach (var cactus in Cactus.ToArray())
        {
            cactus.transform.position -= deltatime * GameState.SpeedMove * Vector3.forward;

            if (cactus.transform.position.z <= OutViewPoint.position.z)
            {
                Cactus.Remove(cactus);
                Destroy(cactus);
            }
        }
    }
}