using UnityEngine;
using Zenject;

[RequireComponent(typeof(LineRenderer))]
public class CutLineView : MonoBehaviour
{
    [SerializeField] private LineRenderer _renderer;
    private const int LeftMouseButton = 0;

    private Camera _camera;
    private Transform _player;

    private Vector3 _startLineLocalPosition;
    private Vector3 _endLineLocalPosition;

    private bool _isDragging;

    private void OnValidate()
    {
        _renderer ??= GetComponent<LineRenderer>();
    }

    protected void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            _isDragging = true;
            SetStartLinePosition();
        }

        if (_isDragging)
        {
            VisualizeLine(true);
            UpdateEndMousePosition();
        }
        else
        {
            VisualizeLine(false);
        }

        if (Input.GetMouseButtonUp(LeftMouseButton))
            _isDragging = false;
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _player = player.Movable.Transform;
    }

    public void SetColor(Color color)
    {
        _renderer.endColor = color;
        _renderer.startColor = new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }

    private void SetStartLinePosition()
    {
        Vector3 worldPos = GetMouseWorldPosition();
        // Сохраняем локальную позицию относительно игрока
        _startLineLocalPosition = _player.InverseTransformPoint(worldPos);
    }

    private void UpdateEndMousePosition()
    {
        Vector3 worldPos = GetMouseWorldPosition();
        // Сохраняем локальную позицию относительно игрока
        _endLineLocalPosition = _player.InverseTransformPoint(worldPos);
    }

    private void VisualizeLine(bool isOn)
    {
        if (_renderer == null)
            return;

        _renderer.enabled = isOn;

        if (isOn)
        {
            _renderer.positionCount = 2;

            // Преобразуем локальные позиции обратно в мировые
            Vector3 startWorldPos = _player.TransformPoint(_startLineLocalPosition);
            Vector3 endWorldPos = _player.TransformPoint(_endLineLocalPosition);

            _renderer.SetPosition(0, startWorldPos);
            _renderer.SetPosition(1, endWorldPos);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _camera.nearClipPlane + 0.05f;
        return _camera.ScreenToWorldPoint(mousePos);
    }
}