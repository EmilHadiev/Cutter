using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class EnemyParryer : MonoBehaviour
{
    [SerializeField] private ParticlePosition _parryParticlePosition;
    [SerializeField] private ParticlePosition _stunParticlePosition;

    private EnemyParryView _parryView;

    private IEnemy _enemy;
    private IDefensible _defender;
    private IEnemyStateMachine _stateMachine;
    private IEnemyAnimator _animator;
    private IFactory _factory;
    private ICutMouseBehaviour _mouseBehaviour;
    private ISoundContainer _soundContainer;

    private bool _isParried;

    private void OnEnable()
    {
        _mouseBehaviour.CutEnded += TryParry;
    }

    private void OnDisable()
    {
        _mouseBehaviour.CutEnded -= TryParry;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _defender = _enemy.Defender;
        _stateMachine = _enemy.StateMachine;
        _animator = _enemy.Animator;
        _parryView = new EnemyParryView(_soundContainer, _factory, _animator, _parryParticlePosition, _stunParticlePosition);

        _defender.ShieldBroke += OnShieldBroke;
    }

    private void OnDestroy()
    {
        _defender.ShieldBroke -= OnShieldBroke;
    }

    [Inject]
    private void Constructor(ISoundContainer soundContainer, IFactory factory, ICutMouseBehaviour mouseBehaviour)
    {
        _factory = factory;
        _mouseBehaviour = mouseBehaviour;
        _soundContainer = soundContainer;
    }

    private void TryParry()
    {
        if (_isParried == false)
            return;

        _parryView.Show();
        _defender.Deactivate();

        SwitchState();
    }

    private void SwitchState()
    {
        _stateMachine.SaveCurrentState();
        _stateMachine.SwitchState<EnemyStateStun>();
    }

    private void ParryTimeStarted()
    {
        _isParried = true;
    }

    private void ParryTimeEnded()
    {
        _isParried = false;
    }

    private void StunEnded()
    {
        _stateMachine.LoadSavedState();
        _parryView.Stop();
        _defender.Activate();
    }
    private void OnShieldBroke()
    {
        _parryView.Stop();
        enabled = false;
    }
}