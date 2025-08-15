using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy", fileName = "EnemyData")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
}