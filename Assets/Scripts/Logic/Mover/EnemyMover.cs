using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMover : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;

    private const float UpdateInterval = 0.1f;

    private IMover _mover;
    private FloatProperty _moveSpeed;
    private EnemyData _data;
    private IPlayer _player;
    
    private float _timer;

    public Transform Transform => transform;
    public FloatProperty MoveSpeed => _moveSpeed;

    private void OnValidate()
    {
        _agent ??= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _data = GetComponent<IEnemy>().Data;

        _moveSpeed = new FloatProperty(_data.Speed);
        SetMove(new EnemyMoveToPlayerPattern(_agent, _moveSpeed, _player.Movable.Transform, transform));
        Debug.Log("?");
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
        _mover.StartMove();
    }

    public void StopMove()
    {
        _mover.StopMove();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
_mover.Update();
        if (_timer >= UpdateInterval)
        {
            
            _timer = 0f;
        }
    }
}