using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    [SerializeField] private bool _setRandomColor = true;
    [SerializeField] private Renderer _render;

    private const string ColorProperty = "_Color";

    private MaterialPropertyBlock _material;

    private void OnValidate()
    {
        _render ??= GetComponent<Renderer>();
    }

    private void Awake()
    {
        _material = new MaterialPropertyBlock();
    }

    void Start()
    {
        if (_setRandomColor == false)
            return;

        _render = GetComponent<Renderer>();
        SetColor(GetRandomColor());
    }

    private Color GetRandomColor()
    {
        return new Color(
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f),
            Random.Range(0.5f, 1f),
            1f
        );
    }

    public void SetColor(Color color)
    {
        _material.SetColor(ColorProperty, color);
        _render.SetPropertyBlock(_material);
    }
}