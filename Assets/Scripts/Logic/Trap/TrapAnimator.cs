using DG.Tweening;
using UnityEngine;

public class TrapAnimator : MonoBehaviour
{
    private const float Y = -0.25f;
    private const int Duration = 1;

    private void Start()
    {
        transform.DOLocalMoveY(Y, Duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}