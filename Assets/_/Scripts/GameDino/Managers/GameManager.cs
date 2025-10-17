using UnityEngine;

public class GameManager : BGCGameManager<GameData>
{
    [field: SerializeField] public GameState GameState { get; private set; }

    public override void Initialize(GameData _gameData)
    {
        GameState = new GameState(this);
        Random.InitState(GameState.SeedRandom);
        base.Initialize(GameData);
    }
}