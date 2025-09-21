using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ButtonUnlock : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private SkinContainer _skinContainer;
    [SerializeField] private TMP_Text _priceText;

    [Inject]
    private readonly IUISoundContainer _sound;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
        _priceText ??= GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable() => _button.onClick.AddListener(TryUnlock);
    private void OnDisable() => _button.onClick.RemoveListener(TryUnlock);
    
    private void TryUnlock()
    {
        if (_skinContainer.TryUnlockRandomSkin())
            _sound.Play(SoundsName.UnlockSkin);

        UpdatePrice();
    }

    public void UpdatePrice() => _priceText.text = _skinContainer.GetCurrentPrice().ToString();
}