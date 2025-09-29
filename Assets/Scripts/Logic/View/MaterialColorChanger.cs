using UnityEngine;

public class MaterialColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _render;
    [SerializeField] private Color[] _colors;

    private const string ColorProperty = "_Color";

    private void OnValidate()
    {
        _render ??= GetComponent<Renderer>();
    }

    public void SetColors(Color[] colors)
    {
        _colors = colors;
        SetColors();
    }

    [ContextMenu(nameof(SetColors))]
    public void SetColors()
    {
        if (IsValid() == false)
        {
            Debug.LogError("Wrong color or materials size!");
            return;
        }

        for (int i = 0; i < _colors.Length; i++)
        {
            SetColor(i, _colors[i]);
        }
    }

    private void SetColor(int materialIndex, Color color)
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

        // ВАЖНО: сначала получаем текущие свойства
        _render.GetPropertyBlock(propertyBlock, materialIndex);

        // Затем устанавливаем цвет (сохраняя остальные свойства)
        propertyBlock.SetColor(ColorProperty, color);

        _render.SetPropertyBlock(propertyBlock, materialIndex);
    }

    [ContextMenu(nameof(ResetColor))]
    public void ResetColor()
    {
        for (int i = 0; i < _render.sharedMaterials.Length; i++)
        {
            _render.SetPropertyBlock(null, i);
        }
    }

    private bool IsValid()
    {
        return _colors.Length == _render.sharedMaterials.Length; // Используем sharedMaterials
    }
}
