using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DefeatStateUI : UiState
{
    [SerializeField] private Button _continueButton;

    [Inject] private readonly PlayerProgress _progress;

    public override void Show()
    {
        base.Show();
        TryHideContinueButton();
    }

    private void TryHideContinueButton()
    {
        if (_progress.IsHardcoreMode)
            _continueButton.gameObject.SetActive(false);
    }
}