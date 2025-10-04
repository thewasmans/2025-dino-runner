public abstract class BGCManager : BGCBaseManager<BGCGameManager>
{
    public BGCGameManager GameManager { get; private set; }

    public override void Initialize(BGCGameManager gameManager)
    {
        GameManager = gameManager;
    }
}