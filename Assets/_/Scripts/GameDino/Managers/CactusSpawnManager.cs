using NaughtyAttributes;
using UnityEngine;

public class CactusSpawnManager : SpawnTimer
{
    public override void Initialize()
    {
        Initialize(GameData.WaitingTimeSpawnCactus);
    }

    [Button]
    public override GameObject InstantiatePrefab()
    {
        float sizeCactus = GameData.SizeCactus;
        GameObject instanceBush = new("BushCactus");

        int quantityCactus = Random.Range(GameData.MinQuantityCactus, GameData.MaxQuantityCactus + 1);

        for (int i = 0; i < quantityCactus; i++)
        {
            InstantiateCactus(instanceBush.transform, i * sizeCactus * Vector3.forward, Random.Range(0, 2) == 0);
        }

        instanceBush.transform.position = SpawnPoint.transform.position;
        instanceBush.transform.parent = ParentInstances;

        return instanceBush;
    }

    public GameObject InstantiateCactus(Transform parent, Vector3 position, bool flipped = false)
    {
        return Instantiate(Prefab,
            position,
            Quaternion.Euler((flipped ? 1 : 0) * 180 * Vector3.up),
            parent);
    }

    public override void UpdateInstances(float deltaTime)
    {
        foreach (var cactus in Instances)
        {
            cactus.transform.position -= deltaTime * GameState.SpeedMove * Vector3.forward;

            if (cactus.transform.position.z <= OutViewPoint.position.z)
            {
                DestroyInstance(cactus);
            }
        }
    }
}