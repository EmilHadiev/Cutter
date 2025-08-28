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

    private ParticleView _followerParticle;
    private ParticleView _startParticle;

    private bool _isWorking = false;

    public CutView(IFactory factory, ICutMouseBehaviour cutMouseBehaviour, IMousePosition mousePosition)
    {
        _factoryService = factory;
        _mouseBehavior = cutMouseBehaviour;
        _mousePosition = mousePosition;
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

        _followerParticle.transform.position = _mousePosition.GetMousePosition();
    }

    private void OnCutStarted()
    {
        _startParticle.transform.position = _mousePosition.GetMousePosition();
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
        result.Stop();

        return result;
    }
}