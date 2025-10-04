public class GameManager : BGCGameManager
{
    public GameState GameState;

    public override void Initialize(BGCGameData _gameData)
    {
        base.Initialize(GameData);
        GameState = new GameState(this);
    }
}
