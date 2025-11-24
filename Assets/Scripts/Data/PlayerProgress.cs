using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.CharacterBaseDataPath + "/" + ScriptableObjectPath.PlayerProgress, fileName = ScriptableObjectPath.PlayerProgress)]
public class PlayerProgress : ScriptableObject
{
    [field: SerializeField] public int CurrentLevel { get; set; }
    [field: SerializeField] public int CurrentHardcoreLevel { get; set; }
    [field: SerializeField] public bool IsHardcoreOpen { get; set; }
    [field: SerializeField] public bool IsHardcoreActivate { get; set; }

    [SerializeField] public int RewardCount = 100;

    public bool IsHardcoreComplete => CurrentHardcoreLevel >= RewardCount;

    public bool IsHardcoreMode => IsHardcoreActivate && IsHardcoreOpen;
}