using DG.Tweening;
using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private ParticlePosition _particlePosition;
    [SerializeField] private SwordPosition _swordPosition;
    [SerializeField] private SwordLookAtPosition _lookAtPosition;

    private IMousePosition _mousePosition;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;
    private SwordAnimation _swordAnimation;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _isCutting;

    private void OnValidate()
    {
        _particlePosition ??= GetComponentInChildren<ParticlePosition>();
    }

    private void Start()
    {
        _swordAnimation = new SwordAnimation(transform, _swordPosition);

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

    private void Look()
    {
        Vector3 direction = _mousePosition.GetMousePosition();

        if (direction == Vector3.zero)
            transform.LookAt(_lookAtPosition.transform);
        else
            transform.LookAt(direction);
    }

    private void StartCutAnimation()
    {
        _isCutting = true;
        _swordAnimation.Play(_startPosition, _endPosition, EndCut);
    }

    private void EndCut()
    {
        _swordAnimation.Stop(_swordView.Deactivate);
        _isCutting = false;
    }
}