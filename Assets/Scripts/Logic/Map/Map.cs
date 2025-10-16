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

    public void SetColor(MapColor mapColor)
    {
        _groundColors.SetColors(mapColor.GroundColors);
        _obstacleColors.SetColors(mapColor.ObstacleColors);

        ChangeColors();
    }

    [ContextMenu(nameof(ChangeColors))]
    private void ChangeColors()
    {
        _groundColors.ChangeColor();
        _obstacleColors.ChangeColor();
    }
}