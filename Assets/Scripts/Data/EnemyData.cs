using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.Enemy)]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 5;
    [field: SerializeField] public int Damage { get; private set; } = 10;
    [field: SerializeField] public int Radius { get; private set; } = 3;
    [field: SerializeField] public int DefenseChance { get; private set; } = 100;
    [field: SerializeField] public AssetReference AssetReference { get; private set; }
}