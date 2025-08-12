using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class CharacterCutLogic : IInitializable, IDisposable, ITickable
{
    private const int MaxTargets = 1;
    private const int AttackDistance = 100;

    private readonly ICutMouseBehaviour _mouseBehaviour;
    private readonly Camera _camera;
    private readonly RaycastHit[] _targets;
    private readonly LayerMask _enemyMask;

    private int _countTargets;

    private bool _isWorking = false;

    public CharacterCutLogic(ICutMouseBehaviour mouseBehaviour)
    {
        _mouseBehaviour = mouseBehaviour;
        _camera = Camera.main;
        _targets = new RaycastHit[MaxTargets];
        _enemyMask = LayerMask.GetMask(CustomMasks.Enemy);
    }

    public void Initialize()
    {
        _mouseBehaviour.CutStarted += StartCutting;
        _mouseBehaviour.CutEnded += StopCutting;
    }

    public void Dispose()
    {
        _mouseBehaviour.CutStarted -= StartCutting;
        _mouseBehaviour.CutEnded -= StopCutting;
    }

    public void Tick()
    {
        if (_isWorking == false || _countTargets >= MaxTargets)
            return;

        SetTargets();
    }

    private void StartCutting()
    {
        _isWorking = true;
        ClearTargets();
    }

    private void StopCutting()
    {
        _isWorking = false;
        ClearTargets();
    }

    private void SetTargets()
    {
        _countTargets = GetCountTargets();
        
        for (int i = 0; i < _countTargets; i++)
        {            
            if (_targets[i].collider.TryGetComponent(out IEnemy enemy))
                enemy.CharacterCut.ActivateCut();

            Debug.Log(_targets[i].collider.name);
        }
    }

    private int GetCountTargets()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastNonAlloc(ray, _targets, AttackDistance, _enemyMask);
    }

    private void ClearTargets()
    {
        Array.Clear(_targets, 0, _targets.Length);
        _countTargets = 0;
    }
}