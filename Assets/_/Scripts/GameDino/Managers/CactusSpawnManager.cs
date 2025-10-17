using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class CactusSpawnManager : Manager
{
    [field: SerializeField] public GameObject Prefab_Cactus { get; private set; }
    [field: SerializeField] public Transform ParentBushCactus { get; private set; }
    [field: SerializeField] public Transform SpawnPoint { get; private set; }
    [field: SerializeField] public Transform OutViewPoint { get; private set; }
    [field: SerializeField] public Timer Timer { get; private set; }
    public List<GameObject> Cactus = new();

    public override void Initialize()
    {
        Timer.Timeout += () => SpawnRandomBushCactus();
        Timer.StartTimer(GameData.WaitingTimeToSpawnCactus);
        GetManager<TimeManager>().TimeUpdated += deltaTime =>
        {
            Timer.Progress(deltaTime);
            UpdateCactus(deltaTime);
        };
    }

    [Button]
    public void SpawnRandomBushCactus()
    {
        float sizeCactus = GameData.SizeCactus;
        GameObject instanceBush = new("BushCactus");

        int quantityCactus = Random.Range(GameData.MinQuantityCactus, GameData.MaxQuantityCactus + 1);

        for (int i = 0; i < quantityCactus; i++)
        {
            InstantiateCactus(instanceBush.transform, i * sizeCactus * Vector3.forward, Random.Range(0, 2) == 0);
        }

        instanceBush.transform.position = SpawnPoint.transform.position;
        instanceBush.transform.parent = ParentBushCactus;

        Cactus.Add(instanceBush);
    }
    public GameObject InstantiateCactus(Transform parent, Vector3 position, bool flipped = false)
    {
        return Instantiate(Prefab_Cactus,
            position,
            Quaternion.Euler((flipped ? 1 : 0) * 180 * Vector3.up),
            parent);
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