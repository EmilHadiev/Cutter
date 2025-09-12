using Cysharp.Threading.Tasks;
using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private SwordPosition _swordPosition;
    [SerializeField] private SwordLookAtPosition _lookAtPosition;

    private ParticlePosition _particlePosition;
    private IMousePosition _mousePosition;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;
    private SwordAnimation _swordAnimation;
    private IFactory _factory;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _isCutting;

    private void Start()
    {
        CreateSword().Forget();

        _cutMouseBehaviour.CutStarted += OnCutStarted;
        _cutMouseBehaviour.CutEnded += OnCutEnded;
    }

    private async UniTaskVoid CreateSword()
    {
        var prefab = await _factory.Create(AssetProvider.SkeletonSword);

        Quaternion rotation = Quaternion.Euler(90, 0, 0);
        prefab.transform.parent = transform;
        prefab.transform.SetLocalPositionAndRotation(Vector3.zero, rotation);

        _particlePosition = prefab.GetComponentInChildren<ParticlePosition>();

        _swordView = new SwordView(_factory, _particlePosition.transform);
        _swordAnimation = new SwordAnimation(transform, _swordPosition);
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
        _factory = factory;
        _mousePosition = cutView;
        _cutMouseBehaviour = cutMouseBehaviour;
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