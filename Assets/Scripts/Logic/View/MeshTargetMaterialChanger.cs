using DynamicMeshCutter;
using UnityEngine;

[RequireComponent(typeof(MeshTarget))]
public class MeshTargetMaterialChanger : MonoBehaviour
{
    [SerializeField] private MeshTarget _target;
    [SerializeField] private MeshRenderer _renderer;

    private void OnValidate()
    {
        _target ??= GetComponent<MeshTarget>();
        _renderer ??= GetComponent<MeshRenderer>();
    }

    public void SetMaterialColor(Color color)
    {
        _target.OverrideFaceMaterial.color = color;
        Debug.Log(_renderer.material);
    }

    public void ChangeMaterial()
    {
        _target.OverrideFaceMaterial = _renderer.material;       
    }
}
