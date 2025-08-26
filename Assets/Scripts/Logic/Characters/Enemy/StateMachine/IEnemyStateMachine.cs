public interface IEnemyStateMachine
{
    void SwitchState<T>() where T : IEnemyState;
    void ExitAllStates();
}