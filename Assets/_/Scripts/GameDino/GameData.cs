using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameDino/GameData", order = 0)]

public class GameData : BGCGameData
{
    [field: SerializeField, Header("Main Data")] public float DefaultSpeedMove { get; private set; } = 2f;
    [field: SerializeField, Header("Character Controller Data")] public float SpeedJump { get; private set; } = 10.0f;
    [field: SerializeField] public float SpeedFall { get; private set; } = 8.0f;
    [field: SerializeField] public float SpeedCrouch { get; private set; } = 10.0f;
    [field: SerializeField] public float MaxVertical { get; private set; } = 2.0f;
    [field: SerializeField] public float MinVertical { get; private set; } = 0.0f;
    [field: SerializeField, Header("Character Controller Data")] public float VerticalSizeCharacter { get; private set; } = 2.0f;
    [field: SerializeField] public float WaitingTimeToSpawnCactus { get; private set; } = 3f;
}
