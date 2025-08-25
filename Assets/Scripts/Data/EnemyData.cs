using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.Enemy)]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public AssetReference AssetReference { get; private set; }
}