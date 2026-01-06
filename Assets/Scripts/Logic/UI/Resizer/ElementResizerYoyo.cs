using DG.Tweening;
using UnityEngine;

public class ElementResizerYoyo : MonoBehaviour
{
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _scale = 1.25f;

    public void PlayAnimation()
    {
        transform.DOScale(transform.localScale * _scale, _duration)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
