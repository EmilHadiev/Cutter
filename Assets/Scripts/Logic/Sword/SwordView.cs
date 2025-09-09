using Cysharp.Threading.Tasks;
using UnityEngine;

public class SwordView
{
    private readonly IFactory _factory;
    private readonly Transform _particlePosition;

    private ParticleView _particle;

    public SwordView(IFactory factory, Transform _particlePostion) 
    { 
        _factory = factory;
        _particlePosition = _particlePostion;
        CreateParticle().Forget();        
    }

    private async UniTaskVoid CreateParticle()
    {
        var prefab = await _factory.Create(AssetProvider.FireParticle, default, default, _particlePosition);
        _particle = prefab.GetComponent<ParticleView>();
        _particle.Stop();
        _particle.transform.position = _particlePosition.position;

        LayerChanger.SetLayerRecursively(_particle.gameObject, LayerMask.NameToLayer(CustomMasks.Player));
    }
    public void Show() => _particle.Play();
    public void Deactivate() => _particle.Stop();
}