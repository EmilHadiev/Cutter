using UnityEngine;

public class SwordPrefab : MonoBehaviour
{
    [field: SerializeField] public Quaternion Rotation { get; private set; }

    [ContextMenu(nameof(SetRotation))]
    private void SetRotation()
    {
        Rotation = transform.rotation;
    }
}