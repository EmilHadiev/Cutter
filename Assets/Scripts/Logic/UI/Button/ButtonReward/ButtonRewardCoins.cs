using TMPro;
using UnityEngine;
using Zenject;

public class ButtonRewardCoins : ButtonReward
{
    [SerializeField] private TMP_Text _text;

    private CoinsCalculator _coinsCalculator;
    private ICoinsStorage _coinsStorage;

    private void Start()
    {
        _text.text = _coinsCalculator.CoinsReward.ToString();
    }

    [Inject]
    private void Constructor(CoinsCalculator calculator, ICoinsStorage coinsStorage)
    {
        _coinsCalculator = calculator;
        _coinsStorage = coinsStorage;
    }

    protected override void SetReward() => _coinsStorage.AddCoins(_coinsCalculator.CoinsReward);
}
