using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwordTemplateView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _hideImage;

    private const string UI = "UI";

    private SwordData _data;
    private GameObject _prefab;

    public event Action<SwordData> Clicked;

    public void Init(SwordData data, GameObject prefab)
    {
        _data = data;
        _prefab = prefab;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_data.IsPurchased == false)
            return;

        Clicked?.Invoke(_data);
    }

    public void Render()
    {
        if (_data.IsPurchased)
            _hideImage.gameObject.SetActive(false);

        LayerChanger.SetLayerRecursively(_prefab, LayerMask.NameToLayer(UI));
        SetPrefabViewData();
    }

    private void SetPrefabViewData()
    {
        _prefab.transform.position = _data.ViewPosition;
        _prefab.transform.rotation = Quaternion.Euler(_data.ViewRotation.x, _data.ViewRotation.y, _data.ViewRotation.z);
        _prefab.transform.localScale = new Vector3(_data.ViewScale, _data.ViewScale, _data.ViewScale);
    }
}