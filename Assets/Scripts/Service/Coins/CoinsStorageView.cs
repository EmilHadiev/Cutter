using TMPro;
using UnityEngine;
using Zenject;

public class CoinsStorageView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    [Inject]
    private readonly ICoinsStorage _coins;

    private void OnValidate() => _text ??= GetComponentInChildren<TMP_Text>();

    private void OnEnable() => _coins.CoinsChanged += OnCoinsChanged;

    private void OnDisable() => _coins.CoinsChanged -= OnCoinsChanged;

    private void Start() => _coins.AddCoins(0);

    private void OnCoinsChanged(int coins) => _text.text = coins.ToString();
}