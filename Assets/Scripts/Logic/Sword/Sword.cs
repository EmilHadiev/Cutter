using DG.Tweening;
using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private ParticlePosition _particlePosition;
    [SerializeField] private SwordBladeRotator _bladeRotator;
    [SerializeField] private float _cutAnimationDuration = 0.25f;

    private IMousePosition _mousePosition;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;

    private Vector3 _startCutPosition;
    private Vector3 _endCutPosition;

    private bool _isCutting;
    private bool _isAnimating;

    private void OnValidate()
    {
        _particlePosition ??= GetComponentInChildren<ParticlePosition>();
    }

    private void OnEnable()
    {
        _cutMouseBehaviour.CutStarted += OnCutStarted;
        _cutMouseBehaviour.CutEnded += OnCutEnded;
    }

    private void OnDisable()
    {
        _cutMouseBehaviour.CutStarted -= OnCutStarted;
        _cutMouseBehaviour.CutEnded -= OnCutEnded;
    }

    private void Update()
    {
        if (_isAnimating)
            return;

        if (!_isCutting)
            FollowToMouse();
    }

    [Inject]
    private void Constructor(IMousePosition cutView, IFactory factory, ICutMouseBehaviour cutMouseBehaviour)
    {
        _mousePosition = cutView;
        _cutMouseBehaviour = cutMouseBehaviour;
        _swordView = new SwordView(factory, _particlePosition.transform);
    }

    private void OnCutStarted()
    {
        _isCutting = true;
        _startCutPosition = _mousePosition.GetMousePosition();
        _swordView.Show();
    }

    private void OnCutEnded()
    {
        _isCutting = false;
        _endCutPosition = _mousePosition.GetMousePosition();

        // Запускаем анимацию разрезания
        StartCutAnimation();
    }

    private void StartCutAnimation()
    {
        _isAnimating = true;
        _swordView.Deactivate();

        // Сохраняем текущую позицию для анимации
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // Вычисляем направление разреза
        Vector3 cutDirection = (_endCutPosition - _startCutPosition).normalized;

        // Анимация движения меча по траектории разреза
        Sequence cutSequence = DOTween.Sequence();

        // Поворачиваем меч в направлении разреза
        cutSequence.Append(transform.DOLookAt(_endCutPosition, _cutAnimationDuration * 0.3f, AxisConstraint.None))
                  .AppendCallback(() => {
                      Rotate();
                  })
                  .OnComplete(() => {
                      _isAnimating = false;
                      OnCutAnimationComplete();
                  });
    }

    private void OnCutAnimationComplete()
    {
        // Возвращаем управление после завершения анимации
        _isAnimating = false;
    }

    private void Rotate()
    {
        if (!_isAnimating)
            transform.rotation = _bladeRotator.GetRotateTowardsMouse(_mousePosition.GetMousePosition());
    }

    private void FollowToMouse()
    {
        if (!_isAnimating)
            transform.LookAt(_mousePosition.GetMousePosition());
    }
}