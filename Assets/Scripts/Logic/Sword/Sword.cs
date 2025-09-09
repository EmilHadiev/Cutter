using DG.Tweening;
using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private ParticlePosition _particlePosition;
    [SerializeField] private SwordPosition _swordPosition;
    [SerializeField] private Quaternion _defaultRotation = new Quaternion(-30, 0,0, 110);

    private const float CutDuration = 0.5f;

    private IMousePosition _mousePosition;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _isCutting;

    private Vector3 SwordPosition => _swordPosition.transform.position;

    private void OnValidate()
    {
        _particlePosition ??= GetComponentInChildren<ParticlePosition>();
    }

    private void Start()
    {
        _cutMouseBehaviour.CutStarted += OnCutStarted;
        _cutMouseBehaviour.CutEnded += OnCutEnded;
    }

    private void OnDestroy()
    {
        _cutMouseBehaviour.CutStarted -= OnCutStarted;
        _cutMouseBehaviour.CutEnded -= OnCutEnded;
    }

    private void Update()
    {
        if (_isCutting)
            return;

        Look();
    }

    [Inject]
    private void Constructor(IMousePosition cutView, IFactory factory, ICutMouseBehaviour cutMouseBehaviour)
    {
        _mousePosition = cutView;
        _cutMouseBehaviour = cutMouseBehaviour;
        _swordView = new SwordView(factory, _particlePosition.transform);
    }

    private void Look()
    {
        Vector3 direction = _mousePosition.GetMousePosition();

        if (direction == Vector3.zero)
            transform.rotation = _defaultRotation;
        else
            transform.LookAt(direction);
    }

    private void OnCutStarted()
    {
        _startPosition = _mousePosition.GetMousePosition();
        _swordView.Show();
    }

    private void OnCutEnded()
    {
        _endPosition = _mousePosition.GetMousePosition();
        transform.position = _startPosition;
        StartCutAnimation();
    }

    private void StartCutAnimation()
    {
        _isCutting = true;

        // Создаем последовательность анимаций
        Sequence cutSequence = DOTween.Sequence();

        // 1. Сохраняем текущую позицию и вращение
        Vector3 currentPosition = transform.position;
        Quaternion currentRotation = transform.rotation;

        // 2. Телепортируем меч в начальную позицию резания
        transform.position = _startPosition;
        transform.LookAt(_endPosition);

        // 3. Движение от startPosition до endPosition (разрез)
        cutSequence.Append(transform.DOMove(_endPosition, CutDuration)
            .SetEase(Ease.OutCubic));

        // 4. Немедленное возвращение в defaultPosition
        cutSequence.Append(transform.DOMove(SwordPosition, CutDuration)
            .SetEase(Ease.InBack));

        // 5. Завершение анимации
        cutSequence.OnComplete(() => EndCut());
    }

    private void EndCut()
    {
        transform.position = SwordPosition;
        _swordView.Deactivate();
        transform.rotation = Quaternion.LookRotation(transform.forward);
        _isCutting = false;
    }
}