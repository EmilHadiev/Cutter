using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class TicTacToeField : MonoBehaviour, ICuttable
{
    [SerializeField] private SphereCollider _collider;
    [SerializeField] private SpriteRenderer _render;
    [SerializeField] private Color _selectColor;

    public event Action<bool> Cut;

    private bool _isCorrect;

    private bool _isSelected;

    private void OnValidate()
    {
        _render ??= GetComponent<SpriteRenderer>();
        _collider ??= GetComponent<SphereCollider>();
    }

    public void SetValue(Sprite sprite, bool isCorrect)
    {
        _render.sprite = sprite;
        _isCorrect = isCorrect;
    }

    public void TryActivateCut()
    {
        if (_isSelected)
            return;

        _render.color = _selectColor;
        Cut?.Invoke(_isCorrect);
        _isSelected = true;
    }

    public void DeactivateCut()
    {
        
    }
}
