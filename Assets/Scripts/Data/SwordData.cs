using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.SwordPath + "/" + ScriptableObjectPath.SwordPath)]
public class SwordData : ScriptableObject, IPurchasable
{
    [field: SerializeField] public AssetProvider.Swords Sword { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int Id { get; private set; }
    [field: SerializeField] public bool IsPurchased { get; private set; } = true;
}