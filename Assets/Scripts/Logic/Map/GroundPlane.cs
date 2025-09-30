using UnityEngine;

public class GroundPlane : MonoBehaviour
{
    [SerializeField] private Renderer _render;

    private const string Color = "_Color";

    private void OnValidate()
    {
        _render ??= GetComponent<Renderer>();
    }

    public void SetColor(Color color)
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        propertyBlock.SetColor(Color, color);

        _render.SetPropertyBlock(propertyBlock);
    }
}