using UnityEngine;

public abstract class SkinData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public bool IsPurchased { get; set; } = true;
    [field: SerializeField] public Vector3 ViewPosition { get; private set; }
    [field: SerializeField] public Vector3 ViewRotation { get; private set; }
    [field: SerializeField] public float ViewScale { get; private set; }

    public abstract int CurrenSkin { get; }
}