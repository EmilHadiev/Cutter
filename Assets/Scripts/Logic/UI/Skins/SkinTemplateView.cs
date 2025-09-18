using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SkinTemplateView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _hideImage;

    private const string UI = "UI";

    private SkinData _data;
    private GameObject _prefab;
    private SwordSkinViewer _skinViewer;

    public void Init(SkinData data, GameObject prefab, SwordSkinViewer swordSkinViewer)
    {
        _data = data;
        _prefab = prefab;
        _skinViewer = swordSkinViewer;
        SetPrefabView();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_data.IsPurchased == false)
            return;

        ShowPrefab(_skinViewer, _prefab, _data);
        PerformEvent(_data);
    }

    public void Render()
    {
        if (_data.IsPurchased)
            _hideImage.gameObject.SetActive(false);
    }

    private void SetPrefabView()
    {
        LayerChanger.SetLayerRecursively(_prefab, LayerMask.NameToLayer(UI));
        _prefab.transform.parent = transform;
        _prefab.transform.localPosition = _data.ViewPosition;
        _prefab.transform.localRotation = Quaternion.Euler(_data.ViewRotation.x, _data.ViewRotation.y, _data.ViewRotation.z);
        _prefab.transform.localScale = new Vector3(_data.ViewScale, _data.ViewScale, _data.ViewScale);
    }

    protected abstract void PerformEvent(SkinData skinData);
    protected abstract void ShowPrefab(SwordSkinViewer skinViewer, GameObject prefab, SkinData data);
}