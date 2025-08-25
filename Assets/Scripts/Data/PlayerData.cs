using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.Player)]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public int NumberEnemiesCut { get; set; }
    [field: SerializeField] public int NumberCutObstacles { get; set; }
}