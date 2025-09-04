using System;
using System.Collections.Generic;

public class EnemyStateMachine : IEnemyStateMachine
{
    private readonly Dictionary<Type, IEnemyState> _states = new Dictionary<Type, IEnemyState>();

    private IEnemyState _currentState;
    private IEnemyState _previousState;

    public EnemyStateMachine(IMovable movable, IAttackable attackable, IEnemyAnimator animator)
    {
        _states.Add(typeof(EnemyWalkingState), new EnemyWalkingState(movable));
        _states.Add(typeof(EnemyAttackingState), new EnemyAttackingState(attackable));
        _states.Add(typeof(EnemyVictoryState), new EnemyVictoryState(animator));

        _states.Add(typeof(EnemyStabState), new EnemyStabState());
    }

    public void SwitchState<T>() where T : IEnemyState
    {
        if (_states.TryGetValue(typeof(T), out var value))
        {
            SetState(value);
        }
        else
        {
            throw new ArgumentException($"{typeof(T)} not found!");
        }
    }

    public void ExitAllStates()
    {
        foreach (var state in _states)
            state.Value.Exit();
    }

    public void SaveCurrentState()
    {
        if (_previousState != _currentState)
            _previousState = _currentState;
    }

    public void LoadSavedState()
    {
        SetState(_previousState);
    }

    private void SetState(IEnemyState value)
    {
        _currentState?.Exit();
        _currentState = value;
        _currentState.Enter();
    }
}