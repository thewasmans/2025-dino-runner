public abstract class Manager : BGCManager<BGCGameManager<GameData>, GameData>, BGCInitializer
{
    public GameState GameState => GameManager.GameState;
    public new GameManager GameManager {get; private set;}

    public override void Initialize(BGCGameManager<GameData> gameManager)
    {
        GameManager = gameManager as GameManager;
        base.Initialize(gameManager);
        Initialize();
    }

    public abstract void Initialize();
    public T GetManager<T>() where T : Manager => GameManager.GetManager<T>();
}