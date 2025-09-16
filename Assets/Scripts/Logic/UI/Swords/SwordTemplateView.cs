using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwordTemplateView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _swordImage;
    [SerializeField] private Image _hideImage;

    private SwordData _data;

    public event Action<SwordData> Clicked;

    public void Init(SwordData data) => _data = data;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_data.IsPurchased == false)
            return;

        Clicked?.Invoke(_data);
    }

    public void Render()
    {
        _swordImage.sprite = _data.Sprite;

        if (_data.IsPurchased)
            _hideImage.gameObject.SetActive(false);
    }
}