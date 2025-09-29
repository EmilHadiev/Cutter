using SplineMesh;
using UnityEngine;

[RequireComponent(typeof(GroundColorChanger))]
[RequireComponent(typeof(ObstacleColorChanger))]
public class Map : MonoBehaviour
{
    [SerializeField] private GroundColorChanger _groundColors;
    [SerializeField] private ObstacleColorChanger _obstacleColors;

    public Color BackgroundColor;
    public Spline Spline;

    private void OnValidate()
    {
        Spline ??= GetComponentInChildren<Spline>();

        _groundColors ??= GetComponent<GroundColorChanger>();
        _obstacleColors ??= GetComponent<ObstacleColorChanger>();
    }

    private void Start()
    {
        ChangeColors();
    }

    [ContextMenu(nameof(ChangeColors))]
    private void ChangeColors()
    {
        _groundColors.SetColor();
        _obstacleColors.SetColor();
    }
}