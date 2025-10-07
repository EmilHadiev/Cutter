using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class SwordView
{
    private readonly IFactory _factory;
    private readonly Transform _particlePosition;
    private readonly PlayerData _playerData;

    private ParticleView _particle;

    public SwordView(IFactory factory, Transform _particlePostion, PlayerData data) 
    { 
        _factory = factory;
        _particlePosition = _particlePostion;
        _playerData = data;
        CreateParticle().Forget();        
    }

    public void Show() => _particle.Play();
    public void Deactivate() => _particle.Stop();

    private async UniTaskVoid CreateParticle()
    {
        var prefab = await _factory.CreateAsync(_playerData.Particle.ToString(), default, default, _particlePosition);
        _particle = prefab.GetComponent<ParticleView>();
        _particle.Stop();
        _particle.transform.position = _particlePosition.position;

        LayerChanger.SetLayerRecursively(_particle.gameObject, LayerMask.NameToLayer(CustomMasks.Player));
    }
}