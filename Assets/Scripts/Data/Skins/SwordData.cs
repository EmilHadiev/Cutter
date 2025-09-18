using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.SkinPath + "/" + ScriptableObjectPath.Sword, fileName = ScriptableObjectPath.Sword)]
public class SwordData : SkinData
{
    [field: SerializeField] public AssetProvider.Swords Sword { get; private set; }

    public override int CurrenSkin => (int)Sword;
}
