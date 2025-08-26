using System;
using UnityEngine;

public class EnemyAttackLogic
{
    private const int MaxAttackTargets = 1;

    private readonly Collider[] _targets;
    private readonly EnemyData _data;
    private readonly Transform _transform;

    public EnemyAttackLogic(EnemyData data, Transform transform)
    {
        _targets = new Collider[MaxAttackTargets];
        _transform = transform;
        _data = data;
    }

    public void Attack()
    {
        ClearTargets();

        int countTargets = Physics.OverlapSphereNonAlloc(GetAttackPosition(), _data.Radius, _targets, GetMask());
        PhysicsDebug.DrawDebug(GetAttackPosition(), _data.Radius);

        if (countTargets <= 0)
            return;

        for (int i = 0; i < _targets.Length; i++)
        {
            if (_targets[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_data.Damage);
        }
    }

    private int GetMask()
    {
        return LayerMask.GetMask(CustomMasks.Player);
    }

    private Vector3 GetAttackPosition()
    {
        return _transform.position + _transform.forward;
    }

    private void ClearTargets()
    {
        Array.Clear(_targets, 0, _targets.Length);
    }
}