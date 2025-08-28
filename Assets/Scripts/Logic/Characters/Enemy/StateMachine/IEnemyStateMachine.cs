public interface IEnemyStateMachine
{
    void SwitchState<T>() where T : IEnemyState;
    void ExitAllStates();

    void SaveCurrentState();
    void LoadSavedState();
}