using UnityEngine;

[RequireComponent(typeof(MeshTargetMaterialChanger))]
[RequireComponent(typeof(MaterialColorChanger))]
public class ObstacleColorSetter : MonoBehaviour
{
    [SerializeField] private MaterialColorChanger _colorChanger;
    [SerializeField] private MeshTargetMaterialChanger _materialChanger;

    private Color _color;

    private void OnValidate()
    {
        _colorChanger ??= GetComponent<MaterialColorChanger>();
        _materialChanger ??= GetComponent<MeshTargetMaterialChanger>();
    }

    public void SetColors(Color[] colors)
    {
        _materialChanger.SetMaterialColor(colors[0]);
        _colorChanger.SetColors(colors);
    }
}