using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.Player)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public int NumberEnemiesCut { get; set; } = 1;
    [field: SerializeField] public int NumberCutObstacles { get; set; } = 3;
    [field: SerializeField] public int Health { get; private set; } = 5;
    [field: SerializeField] public int Energy { get; private set; } = 2;
    [field: SerializeField, Range(0, 0.1f)] public float Speed { get; private set; } = 0.5f;
    [field: SerializeField, Range(1, 10)] public float RotationSpeed { get; private set; } = 5f;
}