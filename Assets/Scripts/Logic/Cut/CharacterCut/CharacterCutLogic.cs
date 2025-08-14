using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class CharacterCutLogic : IInitializable, IDisposable, ITickable, ICharacterCutLogic
{
    private const int MaxTargets = 1;
    private const int AttackDistance = 100;

    private readonly ICutMouseBehaviour _mouseBehaviour;
    private readonly Camera _camera;
    private readonly RaycastHit[] _targets;
    private readonly LayerMask _enemyMask;
    private readonly Action[] _deactivateTargets;

    private int _countTargets;
    private int _cutTarget;

    private bool _isWorking = false;

    public event Action<int> CutTargets;

    public CharacterCutLogic(ICutMouseBehaviour mouseBehaviour)
    {
        _mouseBehaviour = mouseBehaviour;
        _camera = Camera.main;
        _targets = new RaycastHit[MaxTargets];
        _deactivateTargets = new Action[MaxTargets];
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
        CutTargets?.Invoke(_cutTarget);
        _cutTarget = 0;

        _isWorking = false;
        DeactivateTargets();
        ClearTargets();
    }

    private void SetTargets()
    {
        _countTargets = GetCountTargets();

        for (int i = 0; i < _countTargets; i++)
        {
            if (_targets[i].collider.TryGetComponent(out IEnemy enemy))
            {
                enemy.CharacterCut.ActivateCut();
                _deactivateTargets[i] = enemy.CharacterCut.DeactivateCut;
                ++_cutTarget;
            }

            Debug.Log(_targets[i].collider.name);
        }
    }

    private int GetCountTargets()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastNonAlloc(ray, _targets, AttackDistance, _enemyMask);
    }

    private void DeactivateTargets()
    {
        foreach (var deactivator in _deactivateTargets)
            deactivator?.Invoke();
    }

    private void ClearTargets()
    {
        Array.Clear(_targets, 0, _targets.Length);
        Array.Clear(_deactivateTargets, 0, _deactivateTargets.Length);

        _countTargets = 0;
    }
}