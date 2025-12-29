using Cysharp.Threading.Tasks;
using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class Sword : MonoBehaviour
{
    [SerializeField] private SwordPosition _swordPosition;
    [SerializeField] private SwordLookAtPosition _lookAtPosition;
    [SerializeField] private Vector3 _defaultRotation;

    private ParticlePosition _particlePosition;
    private IMousePosition _mousePosition;
    private SwordView _swordView;
    private ICutMouseBehaviour _cutMouseBehaviour;
    private SwordAnimation _swordAnimation;
    private IFactory _factory;
    private PlayerData _playerData;
    private IGameOverService _gameOver;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _isCutting;

    private void Start()
    {
        CreateAndSetSword(_playerData.Sword);

        _cutMouseBehaviour.CutStarted += OnCutStarted;
        _cutMouseBehaviour.CutEnded += OnCutEnded;
        _gameOver.Won += HideAfterEndGame;
    }

    private void OnDestroy()
    {
        _cutMouseBehaviour.CutStarted -= OnCutStarted;
        _cutMouseBehaviour.CutEnded -= OnCutEnded;
        _gameOver.Won -= HideAfterEndGame;
    }

    private void Update()
    {
        if (_isCutting)
            return;

        Look();
    }

    [Inject]
    private void Constructor(IMousePosition cutView, IFactory factory, IGameOverService gameOverService,
        ICutMouseBehaviour cutMouseBehaviour, PlayerData data)
    {
        _gameOver = gameOverService;
        _factory = factory;
        _mousePosition = cutView;
        _cutMouseBehaviour = cutMouseBehaviour;
        _playerData = data;
    }

    public void CreateAndSetSword(AssetProvider.Swords sword)
    {
        CreateSword(sword).Forget();
    }

    private async UniTaskVoid CreateSword(AssetProvider.Swords sword)
    {
        var prefab = await _factory.CreateAsync(sword.ToString());

        Quaternion rotation;

        if (prefab.transform.rotation.x == 0)
            rotation = Quaternion.Euler(prefab.transform.rotation.x, _defaultRotation.y, _defaultRotation.z);
        else
            rotation = Quaternion.Euler(_defaultRotation.x, _defaultRotation.y, _defaultRotation.z);


        prefab.transform.parent = transform;
        prefab.transform.SetLocalPositionAndRotation(Vector3.zero, rotation);

        _particlePosition = prefab.GetComponentInChildren<ParticlePosition>();

        _swordView = new SwordView(_factory, _particlePosition.transform, _playerData);
        _swordAnimation = new SwordAnimation(transform, _swordPosition);
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

    private void HideAfterEndGame() => gameObject.SetActive(false);
}