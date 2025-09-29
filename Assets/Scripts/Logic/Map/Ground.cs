using UnityEngine;

[RequireComponent(typeof(MaterialColorChanger))]
public class Ground : MonoBehaviour
{
    [SerializeField] private MaterialColorChanger _colorChanger;

    private void OnValidate()
    {
        _colorChanger = GetComponent<MaterialColorChanger>();
    }

    public void SetColors(Color[] colors) => _colorChanger.SetColors(colors);
}