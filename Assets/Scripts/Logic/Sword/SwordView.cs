using Cysharp.Threading.Tasks;
using UnityEngine;

public class SwordView
{
    private readonly IFactory _factory;
    private readonly Transform _particlePosition;

    private GameObject _particle;

    public SwordView(IFactory factory, Transform _particlePostion) 
    { 
        _factory = factory;
        _particlePosition = _particlePostion;
        CreateParticle().Forget();
    }

    private async UniTaskVoid CreateParticle()
    {
        _particle = await _factory.Create(AssetProvider.FireParticle, default, default, _particlePosition);
        _particle.transform.position = _particlePosition.position;
        _particle.SetActive(false);
    }

    public void Show() => _particle.SetActive(true);
    public void Deactivate() => _particle.SetActive(false);
}