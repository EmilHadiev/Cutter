using UnityEngine;

[RequireComponent(typeof(CharacterCut))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyDefender))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private CharacterCut _characterCut;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyAttacker _attacker;
    [SerializeField] private EnemyData _data;
    [SerializeField] private EnemyDefender _defender;

    private EnemyStateMachine _stateMachine;

    public ICuttable CharacterCut => _characterCut;
    public IHealth Health => _health;
    public IMovable Mover => _mover;
    public EnemyData Data => _data;
    public IEnemyStateMachine StateMachine => _stateMachine;
    public IEnemyAnimator Animator => _animator;
    public IAttackable Attacker => _attacker;
    public IDefensible Defender => _defender;

    private void OnValidate()
    {
        _characterCut ??= GetComponent<CharacterCut>();
        _health ??= GetComponent<Health>();
        _mover ??= GetComponent<EnemyMover>();
        _animator ??= GetComponent<EnemyAnimator>();
        _attacker ??= GetComponent<EnemyAttacker>();
        _defender ??= GetComponent<EnemyDefender>();
    }

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(Mover, Attacker);
    }

    private void Start()
    {
        _stateMachine.ExitAllStates();

        _stateMachine.SwitchState<EnemyWalkingState>();
    }

    public void SetData(EnemyData data)
    {
        _data = data;
    }
}