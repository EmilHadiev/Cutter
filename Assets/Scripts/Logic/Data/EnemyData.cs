using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public AssetReference Prefab { get; private set; }
}