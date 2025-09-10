using Cysharp.Threading.Tasks;
using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class CutView : IInitializable, IDisposable, ITickable
{
    private readonly ICutMouseBehaviour _mouseBehavior;
    private readonly IFactory _factoryService;
    private readonly IMousePosition _mousePosition;
    private readonly Transform _player;

    private ParticleView _followerParticle;
    private ParticleView _startParticle;

    private Vector3 _startParticleLocalPosition;

    private bool _isWorking = false;

    public CutView(IFactory factory, ICutMouseBehaviour cutMouseBehaviour, IMousePosition mousePosition, IPlayer player)
    {
        _factoryService = factory;
        _mouseBehavior = cutMouseBehaviour;
        _mousePosition = mousePosition;
        _player = player.Movable.Transform;
        CreateParticles().Forget();
    }

    public void Initialize()
    {
        _mouseBehavior.SetLineColor(Color.red);
        _mouseBehavior.CutStarted += OnCutStarted;
        _mouseBehavior.CutEnded += OnCutEnded;
    }

    public void Dispose()
    {
        _mouseBehavior.CutStarted -= OnCutStarted;
        _mouseBehavior.CutEnded -= OnCutEnded;
    }

    public void Tick()
    {
        if (_isWorking == false)
            return;

        UpdateStartParticle();
        UpdateFollowingParticle();
    }

    private void UpdateStartParticle() => _startParticle.transform.position = _player.TransformPoint(_startParticleLocalPosition);
    private void UpdateFollowingParticle() => _followerParticle.transform.position = _mousePosition.GetMousePosition();

    private void OnCutStarted()
    {
        _startParticleLocalPosition = _player.InverseTransformPoint(_mousePosition.GetMousePosition());

        _startParticle.transform.position = _startParticleLocalPosition;
        WorkToggle(true);
    }

    private void OnCutEnded()
    {
        WorkToggle(false);
    }

    private void WorkToggle(bool isOn)
    {
        _isWorking = isOn;

        if (isOn)
        {
            _followerParticle.Play();
            _startParticle.Play();
        }
        else
        {
            _followerParticle.Stop();
            _startParticle.Stop();
        }
    }

    private async UniTaskVoid CreateParticles()
    {
        _followerParticle = await CreateParticle(AssetProvider.FireParticle);
        _startParticle = await CreateParticle(AssetProvider.FireParticle);
    }
    
    private async UniTask<ParticleView> CreateParticle(string assetName)
    {
        var particle = await _factoryService.Create(AssetProvider.FireParticle);
        var result = particle.GetComponent<ParticleView>();

        LayerChanger.SetLayerRecursively(result.gameObject, LayerMask.NameToLayer(CustomMasks.Player));

        result.Stop();

        return result;
    }
}