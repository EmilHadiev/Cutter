using UnityEngine;

public class CutLineView
{
    private readonly LineRenderer _renderer;
    private Vector3 _startLinePosition;
    private Vector3 _endLinePosition;

    public CutLineView(LineRenderer renderer)
    {
        _renderer = renderer;
    }

    public void SetStartLinePosition(Vector3 mousePosition) => _startLinePosition = mousePosition;

    public void UpdateEndMousePosition(Vector3 mousePosition) => _endLinePosition = mousePosition;

    public void VisualizeLine(bool isOn)
    {
        if (_renderer == null)
            return;

        _renderer.enabled = isOn;

        if (isOn)
        {
            _renderer.positionCount = 2;
            _renderer.SetPosition(0, _startLinePosition);
            _renderer.SetPosition(1, _endLinePosition);
        }
    }

    public void SetColor(Color color)
    {
        _renderer.endColor = color;
        _renderer.startColor = new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }
}