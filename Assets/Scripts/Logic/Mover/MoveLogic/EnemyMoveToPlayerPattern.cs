using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveToPlayerPattern : IMover
{
    private readonly NavMeshAgent _agent;        
    private readonly FloatProperty _speed;
    private readonly Transform _player;
    private readonly Transform _enemy;
    private readonly IEnemyAnimator _animator;

    private bool _isWorking;

    public EnemyMoveToPlayerPattern(NavMeshAgent agent, FloatProperty speed, Transform player, Transform enemy, IEnemyAnimator animator)
    {
        _agent = agent;
        _speed = speed;
        _player = player;
        _enemy = enemy;
        _animator = animator;
    }

    public void StartMove()
    {
        _speed.Changed += OnSpeedChanged;

        _agent.enabled = true;
        _isWorking = true;
        _animator.PlayRunning();
    }

    public void StopMove()
    {
        _speed.Changed -= OnSpeedChanged;

        _agent.enabled = false;
        _isWorking = false;
        _animator.StopRunning();
    }

    public void Update()
    {
        if (_isWorking == false)
            return;

        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        _agent.destination = _player.position;
        _enemy.LookAt(_player);
    }

    private void OnSpeedChanged(float speed)
    {
        _agent.speed = speed;
    }
}