using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private TriggerObserver _observer;

    private IMover _mover;
    private FloatProperty _moveSpeed;
    private IPlayer _player;
    private IEnemyStateMachine _stateMachine;

    private bool _isWorking;

    public Transform Transform => transform;
    public FloatProperty MoveSpeed => _moveSpeed;

    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<Enemy>();

        var data = enemy.Data;
        var animator = enemy.Animator;
        _stateMachine = enemy.StateMachine;

        _moveSpeed = new FloatProperty(data.Speed);

        SetMove(new EnemyMoveToPlayerPattern(_agent, _moveSpeed, _player.Movable.Transform, transform, animator));
        StopMove();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _player = player;
    }

    public void SetMove(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    public void StartMove()
    {
        _isWorking = true;
        
        _mover.StartMove();
    }

    public void StopMove()
    {
        _isWorking = false;

        _mover.StopMove();
    }

    private void Update()
    {
        if (_isWorking == false)
            return;

        _mover.Update();
    }

    private void OnPlayerEntered(Collider collider)
    {
        _stateMachine.SwitchState<EnemyWalkingState>();
    }
}