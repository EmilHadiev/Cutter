using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;

    private IMover _mover;
    private FloatProperty _moveSpeed;
    private IPlayer _player;

    public Transform Transform => transform;
    public FloatProperty MoveSpeed => _moveSpeed;

    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Awake()
    {
        IEnemy enemy = GetComponent<Enemy>();

        var data = enemy.Data;
        var animator = enemy.Animator;

        data = enemy.Data;
        animator = enemy.Animator;

        if (data == null)
        {
            Debug.Log($"{nameof(data)} is NULL! Set default value 5");
            _moveSpeed = new FloatProperty(5);
        }
        else
        {
            _moveSpeed = new FloatProperty(data.Speed);
        }
        
        SetMove(new EnemyMoveToPlayerPattern(_agent, _moveSpeed, _player.Movable.Transform, transform, animator));
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
        enabled = true;
        
        _mover.StartMove();
    }

    public void StopMove()
    {
        enabled = false;

        _mover.StopMove();
    }

    private void Update()
    {
        _mover.Update();
    }
}