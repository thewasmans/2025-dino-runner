using Unity.Collections;
using UnityEngine;

public class DinoController : BGCManager
{
    [field: SerializeField] public GameObject DinoCharacter { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveUp { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveFall { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveCrouch { get; private set; }
    public Vector3 InitialPosition { get; private set; }
    private GameData _gameData;

    public override void Initialize(BGCGameManager gameManager)
    {
        base.Initialize(gameManager);
        InitialPosition = transform.position;
        if (!(gameManager.GameData is GameData))
        {
            throw new BGCException($"[ {nameof(DinoController)} ] The {nameof(gameManager.GameData)} referenced in the inspector must be a {(nameof(_gameData))}");
        }
        _gameData = GameManager.GameData as GameData;
    }

    void Update()
    {
        if (!MoveUp && Input.GetKey(KeyCode.UpArrow))
        {
            MoveUp = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveCrouch = true;
            MoveFall = false;
            MoveUp = false;
        }

        if (MoveUp)
        {
            if (DinoCharacter.transform.position.y <= _gameData.MaxVertical)
            {
                DinoCharacter.transform.position += Vector3.up * Time.deltaTime * _gameData.SpeedJump;
            }
            else
            {
                MoveUp = false;
                MoveFall = true;
            }
        }
        else if (MoveFall)
        {
            if (DinoCharacter.transform.position.y >= _gameData.MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * _gameData.SpeedFall;
            }
            else
            {
                MoveFall = false;
            }
        }
        else if (MoveCrouch)
        {
            if (DinoCharacter.transform.position.y >= _gameData.MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * _gameData.SpeedCrouch;
            }
            else
            {
                MoveCrouch = false;
            }
        }
    }
}