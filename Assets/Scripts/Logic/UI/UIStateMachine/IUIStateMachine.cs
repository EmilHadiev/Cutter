public interface IUIStateMachine
{
    void Switch<T>() where T : UiState;
}