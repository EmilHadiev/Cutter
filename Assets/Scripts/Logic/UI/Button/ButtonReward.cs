using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonReward : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    private IAdvService _adv;
    private ICoinsStorage _coinsStorage;
    private CoinsCalculator _coinsCalculator;
    private IUISoundContainer _sound;

    private void OnValidate() => _button ??= GetComponent<Button>();

    private void OnEnable()
    {
        _button.onClick.AddListener(AddReward);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(AddReward);
    }

    private void Start()
    {
        _text.text = _coinsCalculator.CoinsReward.ToString();
    }

    [Inject]
    private void Constructor(IAdvService adv, ICoinsStorage coinsStorage, CoinsCalculator coinsCalculator, IUISoundContainer sounds)
    {
        _adv = adv;
        _coinsStorage = coinsStorage;
        _coinsCalculator = coinsCalculator;
        _sound = sounds;
    }

    private void AddReward()
    {
        _adv.ShowReward(ProcessReward);
    }

    private void ProcessReward()
    {
        _coinsStorage.AddCoins(_coinsCalculator.CoinsReward);
        _sound.Stop();
        _sound.Play(SoundsName.AddRewardCoins);
    }
}