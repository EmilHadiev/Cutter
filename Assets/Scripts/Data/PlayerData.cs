using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.Player)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public int NumberEnemiesCut { get; set; } = 1;
    [field: SerializeField] public int NumberCutObstacles { get; set; } = 3;
    [field: SerializeField] public int Health { get; private set; } = 100;
}