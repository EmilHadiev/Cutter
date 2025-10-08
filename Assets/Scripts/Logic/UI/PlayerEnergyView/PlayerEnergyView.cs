using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class PlayerEnergyView : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Image _energyImage;

    private const float VisibleAlpha = 0.5f;
    private const int DefaultAlpha = 0;
    private const float Delay = 0.1f;

    private CancellationTokenSource _showCts;
    private IEnergy _energy;

    private Vector3 _defaultScale;

    private void OnValidate()
    {
        _canvasGroup ??= GetComponent<CanvasGroup>();
        _energyImage ??= GetComponent<Image>();
    }

    private void Awake()
    {
        HideView();
    }

    private void OnEnable() => _energy.EnergyChanged += OnEnergyChanged;

    private void OnDisable() => _energy.EnergyChanged -= OnEnergyChanged;

    private void OnDestroy()
    {
        _showCts?.Cancel();
        _showCts?.Dispose();
    }

    [Inject]
    private void Constructor(IPlayer player) => _energy = player.Energy;

    private void OnEnergyChanged(int currentEnergy, int maxEnergy)
    {
        if (currentEnergy == 0)
            ShowView().Forget();
    }

    private async UniTaskVoid ShowView()
    {
        _showCts?.Cancel();
        _showCts = new CancellationTokenSource();

        try
        {
            _canvasGroup.alpha = VisibleAlpha;
            ShowNotEnoughEnergy();
            await UniTask.Delay(1500, cancellationToken: _showCts.Token);
            HideView();
        }
        catch
        {

        }
    }

    private void ShowNotEnoughEnergy()
    {
        if (DOTween.IsTweening(_energyImage.transform))
            return;

        _energyImage.transform.DOKill();

        // Yoyo с 1 циклом: увеличить -> вернуть
        _energyImage.transform.DOScale(_defaultScale * 1.5f, 0.5f)
            .SetEase(Ease.Linear)
            .SetLoops(2, LoopType.Yoyo); // 2 цикла = 1 полное колебание
    }

    private void HideView() => _canvasGroup.alpha = DefaultAlpha;
}
