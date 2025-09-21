using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class Beginner : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IUIStateMachine _uiStateMachine;

    private void OnValidate()
    {
        _button.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Clicked);
    }

    [Inject]
    private void Constructor(IUIStateMachine uiStateMachine)
    {
        _uiStateMachine = uiStateMachine;
    }

    private void Clicked()
    {
        _uiStateMachine.Switch<GameplayStateUI>();
    }
}