using Cysharp.Threading.Tasks;
using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class CutView : IInitializable, IDisposable, ITickable
{
    private const int ParticleSpeed = 15;

    private readonly ICutMouseBehaviour _mouseBehavior;
    private readonly IFactory _factoryService;
    private readonly IMousePosition _mousePosition;

    private GameObject _endParticle;

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
    }

    private void OnCutStarted()
    {
        _endParticle.transform.position = _mousePosition.GetMousePosition();
        WorkToggle(true);
    }

    private void OnCutEnded()
    {
        WorkToggle(false);
    }

    private void WorkToggle(bool isOn)
    {
        _isWorking = isOn;
        _endParticle.gameObject.SetActive(isOn);
    }

    private async UniTaskVoid CreateParticles()
    {
        _endParticle = await CreateParticle(AssetProvider.FireParticle);
    }

    private async UniTask<GameObject> CreateParticle(string assetName)
    {
        var particle = await _factoryService.Create(AssetProvider.FireParticle);
        particle.gameObject.SetActive(false);

        return particle;
    }
}