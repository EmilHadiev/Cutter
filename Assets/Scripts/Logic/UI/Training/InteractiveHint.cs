using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveHint : HintInfo
{
    [SerializeField] private Image _hintImage;
    [SerializeField] private Transform _finalPoint;
    [SerializeField] private float _delay;

    public override void Show()
    {
        _hintImage.transform.DOMove(_finalPoint.transform.position, _delay)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}