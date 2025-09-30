using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public abstract class BaseCutLogic : IInitializable, IDisposable, ITickable
{
    private readonly float _attackDistance;
    private readonly int _maxTargets;

    private readonly ICutMouseBehaviour _mouseBehaviour;
    private readonly Camera _camera;
    private readonly RaycastHit[] _targets;
    private readonly LayerMask _enemyMask;
    private readonly ICutTargetsCounter _counter;

    private readonly ICuttable[] _deactivateTargets;
    private readonly ICutUpdatable[] _updateTargets;

    private int _countTargets;
    private int _cutTarget;

    private bool _isWorking = false;

    public BaseCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter, PlayerData playerData)
    {
        _mouseBehaviour = mouseBehaviour;
        _camera = Camera.main;       
        _counter = cutTargetsCounter;
        _enemyMask = GetLayerMask();
        _attackDistance = playerData.AttackDistance;

        _maxTargets = GetMaxTargets(playerData);

        _targets = new RaycastHit[_maxTargets];

        _deactivateTargets = new ICuttable[_maxTargets]; 
        _updateTargets = new ICutUpdatable[_maxTargets];
    }

    protected abstract LayerMask GetLayerMask();
    protected abstract int GetMaxTargets(PlayerData playerData);

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
        if (_isWorking == false)
            return;

        if (_countTargets < _maxTargets)
            SetTargets();

        if (_updateTargets.Length > 0)
            UpdateTargets();
    }

    private void StartCutting()
    {
        _isWorking = true;
        ClearTargets();
    }

    private void StopCutting()
    {
        _counter.AddCountTargets(_cutTarget);
        _cutTarget = 0;

        _isWorking = false;
        DeactivateTargets();
        ClearTargets();
    }

    private void SetTargets()
    {
        _countTargets = GetCountTargets();

        if (_countTargets == 0)
            return;

        for (int i = 0; i < _countTargets; i++)
        {
            Collider collider = _targets[i].collider;

            if (collider.TryGetComponent(out ICuttable cuttable))
            {
                cuttable.TryActivateCut();
                _deactivateTargets[i] = cuttable;
                ++_cutTarget;
            }

            if (collider.TryGetComponent(out ICutUpdatable cutUpdatable))
                _updateTargets[i] = cutUpdatable;
        }
    }

    private void UpdateTargets()
    {
        for (int i = 0; i < _updateTargets.Length; i++)
            _updateTargets[i]?.UpdateTargets();
    }

    private int GetCountTargets()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        return Physics.RaycastNonAlloc(ray, _targets, _attackDistance, _enemyMask);
    }

    private void DeactivateTargets()
    {
        for (int i = 0; i < _deactivateTargets.Length; i++)
        {
            _deactivateTargets[i]?.DeactivateCut();
        }
    }

    private void ClearTargets()
    {
        Array.Clear(_targets, 0, _targets.Length);
        Array.Clear(_deactivateTargets, 0, _deactivateTargets.Length);

        _countTargets = 0;
    }
}