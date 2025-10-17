using UnityEngine;

public class CloudsManager : SpawnTimer
{
    public override void Initialize()
    {
        Initialize(GameData.WaitingTimeSpawnClouds);
    }

    public override GameObject InstantiatePrefab()
    {
        return Instantiate(Prefab,
            SpawnPoint.transform.position,
            Quaternion.identity,
            ParentInstances);
    }

    public override void UpdateInstances(float deltaTime)
    {
        foreach (var cloud in Instances)
        {
            cloud.transform.position -= deltaTime * GameState.SpeedMove * Vector3.forward;

            if (cloud.transform.position.z <= OutViewPoint.position.z)
            {
                DestroyInstance(cloud);
            }
        }
    }
}
