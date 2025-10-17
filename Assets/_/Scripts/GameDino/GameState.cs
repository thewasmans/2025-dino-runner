using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class GameState : BGCGameState
{
    [field: SerializeField, ReadOnly] public int SeedRandom { get; private set; }
    [field: SerializeField, ReadOnly] public float SpeedMove { get; private set; }

    public GameState(GameManager gameManager)
    {
        GameData gameData = gameManager.GameData;
        SpeedMove = gameData.DefaultSpeedMove;
        SeedRandom = gameData.DefaultSeedRandom;
    }
}