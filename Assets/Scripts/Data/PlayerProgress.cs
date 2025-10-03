using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.PlayerProgress, fileName = ScriptableObjectPath.PlayerProgress)]
public class PlayerProgress : ScriptableObject
{
    [field: SerializeField] public int CurrentLevel { get; set; }
    [field: SerializeField] public int CurrentHardcoreLevel { get; set; }
    [field: SerializeField] public bool IsHardcoreOpen { get; set; }
}