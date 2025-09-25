using DG.Tweening;
using UnityEngine;

public class TrapAnimator : MonoBehaviour
{
    private const float Y = -0.25f;
    private const int Duration = 1;

    private void Start()
    {
        float y = transform.position.y + Y;

        transform.DOLocalMoveY(y, Duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}