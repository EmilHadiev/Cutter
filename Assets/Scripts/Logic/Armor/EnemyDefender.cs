using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class EnemyDefender : MonoBehaviour, IDefensible
{
    [SerializeField] private Shield _shield;

    private IEnemyAnimator _animator;
    private IEnemyStateMachine _stateMachine;
    private EnemyData _data;

    public bool IsDefending { get; private set; }

    public bool IsShieldExisting => _shield != null;

    private void OnValidate()
    {
        _shield ??= GetComponentInChildren<Shield>();
    }

    private void Awake()
    {
        if (IsShieldExisting == false)
            enabled = false;
    }

    [Inject]
    private ICutMouseBehaviour _mouseBehaviour;

    private void OnEnable()
    {
        _mouseBehaviour.CutEnded += Defended;
    }

    private void OnDisable()
    {
        _mouseBehaviour.CutEnded -= Defended;
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _animator = enemy.Animator;
        _stateMachine = enemy.StateMachine;
        _data = enemy.Data;
    }

    public bool TryDefend()
    {
        if (enabled == false || IsShieldExisting == false)
            return false;

        if (IsDefending)
            return true;

        if (Random.Range(0, 100) <= _data.DefenseChance)
        {            
            StartDefend();
            return true;
        }

        return false;
    }

    private void StartDefend()
    {
        IsDefending = true;
    }

    private void DefenseEnded()
    {
        _animator.ResetDefenseTrigger();
        IsDefending = false;
    }

    private void Defended()
    {
        _animator.StartDefenseTrigger();
    }
}