using UnityEngine;

public class DinoCharacterGizmos : MonoBehaviour
{
    public DinoController DinoController { get; private set; } 
    [field: SerializeField] public GameManager GameManager { get; private set; }

    void OnDrawGizmos()
    {
        if (GameManager == null)
        {
            Debug.LogWarning($"[ {nameof(DinoCharacterGizmos)} ] {nameof(GameManager)} is not referenced in the inspector. Please define a {nameof(GameManager)}");
            return;
        }
        if (GameManager.GameData == null) return;
        GameData GameData = GameManager.GameData as GameData;
        var pos = DinoController ? DinoController.InitialPosition : transform.position;
        Gizmos.color = Color.red;

        float height = GameData.MaxVertical - GameData.MinVertical + GameData.VerticalSizeCharacter;

        var size = Vector3.one * 2 + Vector3.up * (height - 2);
        Gizmos.DrawWireCube(pos + Vector3.up * (GameData.MinVertical + height / 2), size);
    }
}