using UnityEngine;

[RequireComponent(typeof(MaterialColorChanger))]
public class Ground : MonoBehaviour
{
    [SerializeField] private MaterialColorChanger _colorChanger;
    [SerializeField] private GroundPlane _plane;

    private void OnValidate()
    {
        _colorChanger = GetComponent<MaterialColorChanger>();
        _plane = GetComponentInChildren<GroundPlane>();
    }

    public void SetColors(Color[] colors)
    {
        _colorChanger.SetColors(colors);
        _plane.SetColor(colors[1]);
    }
}