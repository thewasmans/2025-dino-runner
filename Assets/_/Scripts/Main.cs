using UnityEngine;

public class Main : MonoBehaviour
{
    [ field:SerializeField] public GameManager GameManager { get; private set; }

    void Awake()
    {
        GameManager.Initialize(null);
    }
}
