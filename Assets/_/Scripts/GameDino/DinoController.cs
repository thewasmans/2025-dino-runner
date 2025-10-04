using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class DinoController : BGCManager
{
    [field: SerializeField] public float SpeedJump { get; private set; } = 3.0f;
    [field: SerializeField] public float SpeedFall { get; private set; } = 2.0f;
    [field: SerializeField] public float SpeedCrouch { get; private set; } = 6.0f;
    [field: SerializeField] public float MaxVertical { get; private set; } = 1.0f;
    [field: SerializeField] public float MinVertical { get; private set; } = 0.0f;
    [field: SerializeField] public GameObject DinoCharacter { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveUp { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveFall { get; private set; }
    [field: SerializeField, ReadOnly] public bool MoveCrouch { get; private set; }

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
            if (DinoCharacter.transform.position.y <= MaxVertical)
            {
                DinoCharacter.transform.position += Vector3.up * Time.deltaTime * SpeedJump;
            }
            else
            {
                MoveUp = false;
                MoveFall = true;
            }
        }
        else if (MoveFall)
        {
            if (DinoCharacter.transform.position.y >= MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * SpeedFall;
            }
            else
            {
                MoveFall = false;
            }
        }
        else if (MoveCrouch)
        {
            if (DinoCharacter.transform.position.y >= MinVertical)
            {
                DinoCharacter.transform.position -= Vector3.up * Time.deltaTime * SpeedCrouch;
            }
            else
            {
                MoveCrouch = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var size = Vector3.one + Vector3.up * MaxVertical;
        Gizmos.DrawWireCube(DinoCharacter.transform.position + Vector3.one, size);

        // Gizmos.DrawCube(Vector3.zero, Vector3.one * 10)
    }
}
