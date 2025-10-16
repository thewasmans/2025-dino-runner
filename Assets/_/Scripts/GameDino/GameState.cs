using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class GameState : BGCGameState
{
    [field: SerializeField, ReadOnly] public float SpeedMove { get; private set; }

    public GameState(GameManager gameManager)
    {
        SpeedMove = gameManager.GameData.DefaultSpeedMove;
    }
}