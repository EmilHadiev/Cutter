using UnityEngine;

[RequireComponent(typeof(CharacterCut))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
[RequireComponent(typeof(EnemyAttacker))]
[RequireComponent(typeof(EnemyDefender))]
[RequireComponent(typeof(EnemyGameOverChecker))]
[RequireComponent(typeof(EnemyParryer))]
[RequireComponent(typeof(EnemyHider))]
[RequireComponent(typeof(Armor))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private CharacterCut _characterCut;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private EnemyAnimator _animator;
    [SerializeField] private EnemyAttacker _attacker;
    [SerializeField] private EnemyData _data;
    [SerializeField] private EnemyDefender _defender;
    [SerializeField] private EnemyParryer _parryer;
    [SerializeField] private Armor _armor;

    private EnemyStateMachine _stateMachine;
    private CutValidator _cutValidator;

    public IHealth Health => _health;
    public IMovable Mover => _mover;
    public EnemyData Data => _data;
    public IEnemyStateMachine StateMachine => _stateMachine;
    public IEnemyAnimator Animator => _animator;
    public IAttackable Attacker => _attacker;
    public IDefensible Defender => _defender;
    public IParryable Parryer => _parryer;
    public IArmorable Armor => _armor;
    public CutValidator Validator => _cutValidator;


    private void OnValidate()
    {
        _characterCut ??= GetComponent<CharacterCut>();
        _health ??= GetComponent<EnemyHealth>();
        _mover ??= GetComponent<EnemyMover>();
        _animator ??= GetComponent<EnemyAnimator>();
        _attacker ??= GetComponent<EnemyAttacker>();
        _defender ??= GetComponent<EnemyDefender>();
        _parryer ??= GetComponent<EnemyParryer>();
        _armor ??= GetComponent<Armor>();
    }

    private void Awake()
    {
        _stateMachine = new EnemyStateMachine(Mover, Attacker, Animator);
        _cutValidator = new CutValidator(new ICutCondition[] {Parryer, Defender, _health, Armor});
    }

    private void Start()
    {
        _stateMachine.SwitchState<EnemyStabState>();
    }

    public void SetData(EnemyData data)
    {
        _data = data;
    }
}