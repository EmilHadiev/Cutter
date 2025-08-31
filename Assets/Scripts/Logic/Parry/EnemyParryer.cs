using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

public class EnemyParryer : MonoBehaviour, IParryable
{
    [SerializeField] private ParticlePosition _parryParticlePosition;
    [SerializeField] private ParticlePosition _stunParticlePosition;

    private EnemyParryView _parryView;

    private IEnemy _enemy;
    private IEnemyStateMachine _stateMachine;
    private IEnemyAnimator _animator;
    private IFactory _factory;
    private ICutMouseBehaviour _mouseBehaviour;
    private ISoundContainer _soundContainer;

    private bool IsActivated => enabled == true;

    private bool _isParryWindowOpen;
    private bool _wasParried;

    public bool IsParryTime => enabled == true && _isParryWindowOpen;

    private void OnEnable()
    {
        _mouseBehaviour.CutEnded += TryParry;
    }

    private void OnDisable()
    {
        _mouseBehaviour.CutEnded -= TryParry;
    }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _stateMachine = _enemy.StateMachine;
        _animator = _enemy.Animator;
        _parryView = new EnemyParryView(_soundContainer, _factory, _animator, _parryParticlePosition, _stunParticlePosition);
    }

    [Inject]
    private void Constructor(ISoundContainer soundContainer, IFactory factory, ICutMouseBehaviour mouseBehaviour)
    {
        _factory = factory;
        _mouseBehaviour = mouseBehaviour;
        _soundContainer = soundContainer;
    }

    public void Activate() => enabled = true;
    public void Deactivate() => enabled = false;

    private void TryParry()
    {
        if (_isParryWindowOpen == false)
        {
            return;
        }

        _parryView.ShowParryImpact();
        _wasParried = true;

        SwitchState();
    }

    private void SwitchState()
    {
        _stateMachine.SaveCurrentState();
        _stateMachine.SwitchState<EnemyStateStun>();
    }

    private void ParryTimeStarted()
    {
        if (IsActivated == false)
            return;

        _parryView.ShowParryWindow();
        _isParryWindowOpen = true;
    }

    private void ParryTimeEnded()
    {
        _parryView.CloseParryWindow();
        _isParryWindowOpen = false;
    }

    private void StunEnded()
    {
        if (_wasParried == false)
            return;

        _wasParried = false;
        _stateMachine.LoadSavedState();
        _parryView.CloseParryImpact();
        Deactivate();
    }
}