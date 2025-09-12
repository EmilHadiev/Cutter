using DG.Tweening;
using System;
using UnityEngine;

public class SwordAnimation
{
    private const float CutDuration = 0.5f;

    private readonly Transform _transform;
    private readonly SwordPosition _swordPosition;

    private Vector3 SwordPosition => _swordPosition.transform.position;

    public SwordAnimation(Transform transform, SwordPosition swordPosition)
    {
        _transform = transform;
        _swordPosition = swordPosition;
    }

    public void Play(Vector3 startPosition, Vector3 endPosition, Action onComplete)
    {
        // Создаем последовательность анимаций
        Sequence cutSequence = DOTween.Sequence();

        // 1. Сохраняем текущую позицию и вращение
        Vector3 currentPosition = _transform.position;
        Quaternion currentRotation = _transform.rotation;

        // 2. Телепортируем меч в начальную позицию резания
        _transform.position = startPosition;
        _transform.LookAt(endPosition);

        // 3. Движение от startPosition до endPosition (разрез)
        cutSequence.Append(_transform.DOMove(endPosition, CutDuration)
            .SetEase(Ease.OutCubic));

        // 4. Немедленное возвращение в defaultPosition
        cutSequence.Append(_transform.DOMove(SwordPosition, CutDuration)
            .SetEase(Ease.InBack));

        // 5. Завершение анимации
        cutSequence.OnComplete(() => onComplete?.Invoke());
    }

    public void Stop(Action deactivate)
    {
        _transform.position = SwordPosition;
        deactivate?.Invoke();
        _transform.rotation = Quaternion.LookRotation(_transform.forward);
    }
}