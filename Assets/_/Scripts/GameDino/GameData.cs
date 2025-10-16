using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "GameDino/GameData", order = 0)]

public class GameData : BGCGameData
{
    [Header("Main Data")]
    [field: SerializeField] public float DefaultSpeedMove { get; private set; } = 2f;
    [Header("Character Controller Data")]
    [field: SerializeField] public float SpeedJump { get; private set; } = 10.0f;
    [field: SerializeField] public float SpeedFall { get; private set; } = 8.0f;
    [field: SerializeField] public float SpeedCrouch { get; private set; } = 10.0f;
    [field: SerializeField] public float MaxVertical { get; private set; } = 2.0f;
    [field: SerializeField] public float MinVertical { get; private set; } = 0.0f;
    [field: SerializeField] public float VerticalSizeCharacter { get; private set; } = 2.0f;
    [Header("Character Controller Data")]
    [field: SerializeField] public float WaitingTimeToSpawnCactus { get; private set; } = 3f;
}
