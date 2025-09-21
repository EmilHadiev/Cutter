using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIStateMachine : MonoBehaviour, IUIStateMachine
{
    [SerializeField] private PauseStateUI _pauseState;
    [SerializeField] private GameplayStateUI _gameplayState;
    [SerializeField] private DefeatStateUI _defeatState;
    [SerializeField] private VictoryStateUI _victoryState;

    private readonly Dictionary<Type, UiState> _states = new Dictionary<Type, UiState>();

    private IGameOverService _gameOverService;

    private void Awake()
    {
        _states.Add(typeof(PauseStateUI), _pauseState);
        _states.Add(typeof(GameplayStateUI), _gameplayState);
        _states.Add(typeof(DefeatStateUI), _defeatState);
        _states.Add(typeof(VictoryStateUI), _victoryState);
    }

    private void OnEnable()
    {
        _gameOverService.Lost += Switch<DefeatStateUI>;
        _gameOverService.Continue += Switch<GameplayStateUI>;
        _gameOverService.Won += Switch<VictoryStateUI>;
    }

    private void OnDisable()
    {
        _gameOverService.Lost -= Switch<DefeatStateUI>;
        _gameOverService.Continue -= Switch<GameplayStateUI>;
        _gameOverService.Won -= Switch<VictoryStateUI>;
    }

    private void Start() => Switch<PauseStateUI>();

    [Inject]
    private void Constructor(IGameOverService gameOver)
    {
        _gameOverService = gameOver;
    }

    public void Switch<T>() where T : UiState
    {
        if (_states.TryGetValue(typeof(T), out var state))
            ShowState(state);
        else
            throw new ArgumentException($"{typeof(T)}");
    }

    private void ShowState(UiState currentState)
    {
        foreach (var state in _states)
        {
            if (state.Value == currentState)
                state.Value.Show();
            else
                state.Value.Hide();
        }
    }
}