using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.SkinPath + "/" + ScriptableObjectPath.Particle, fileName = ScriptableObjectPath.Particle)]
public class ParticleData : SkinData
{
    [field: SerializeField] public AssetProvider.Particles Particle { get; private set; }
}