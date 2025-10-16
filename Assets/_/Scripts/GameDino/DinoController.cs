using Unity.Collections;
using UnityEngine;

public class DinoController : Manager
{
    [field: SerializeField] public GameObject DinoCharacter { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveUp { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveFall { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveCrouch { get; private set; }
    public Vector3 InitialPosition { get; private set; }

    public override void Initialize()
    {
        InitialPosition = transform.position;
    }

    void Update()
    {
        if (!MoveUp && Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveCrouch = true;
            MoveFall = false;
            MoveUp = false;
        }

        if (MoveUp && !MoveFall)
        {
            if (DinoCharacter.transform.position.y <= GameData.MaxVertical)
            {
                DinoCharacter.transform.position += Vector3.up * Time.deltaTime * GameData.SpeedJump;
            }
            else
            {
                MoveUp = false;
                MoveFall = true;
            }
        }
        if (MoveFall)
        {
            if (DinoCharacter.transform.position.y >= GameData.MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * GameData.SpeedFall;
            }
            else
            {
                MoveFall = false;
            }
        }
        else if (MoveCrouch)
        {
            if (DinoCharacter.transform.position.y >= GameData.MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * GameData.SpeedCrouch;
            }
            else
            {
                MoveCrouch = false;
            }
        }
    }
}