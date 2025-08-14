using Cysharp.Threading.Tasks;
using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class CutView : IInitializable, IDisposable, ITickable
{
    private const int DistanceToCamera = 10;
    private const int ParticleSpeed = 15;

    private readonly ICutMouseBehaviour _mouseBehavior;
    private readonly IFactory _factoryService;
    private readonly Camera _camera;

    private GameObject _startParticle;
    private GameObject _endParticle;

    private bool _isWorking = false;

    public CutView(IFactory factory, ICutMouseBehaviour cutMouseBehaviour)
    {
        _factoryService = factory;
        _mouseBehavior = cutMouseBehaviour;
        _camera = Camera.main;
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

        Vector3 targetPos = GetMoisePosition();

        _startParticle.transform.position = Vector3.Lerp(
            _startParticle.transform.position,
            targetPos,
            ParticleSpeed * Time.deltaTime
        );
    }

    private static Vector3 GetMoisePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = DistanceToCamera; // Расстояние от камеры до объекта
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        return targetPos;
    }

    private void OnCutStarted()
    {
        WorkToggle(true);
        _endParticle.transform.position = GetMoisePosition();
    }

    private void OnCutEnded()
    {
        WorkToggle(false);
    }

    private void WorkToggle(bool isOn)
    {
        _isWorking = isOn;
        _endParticle.gameObject.SetActive(isOn);
        _startParticle.gameObject.SetActive(isOn);
    }

    private async UniTaskVoid CreateParticles()
    {
        _startParticle = await CreateParticle(AssetProvider.FireParticle);
        _endParticle = await CreateParticle(AssetProvider.FireParticle);
    }

    private async UniTask<GameObject> CreateParticle( string assetName)
    {
        var particle = await _factoryService.Create(AssetProvider.FireParticle);
        particle.gameObject.SetActive(false);

        return particle;
    }
}