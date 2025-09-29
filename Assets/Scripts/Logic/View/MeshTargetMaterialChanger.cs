using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class MeshTargetMaterialChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private Material _overrideMaterial;

    private void OnValidate()
    {
        _renderer ??= GetComponent<MeshRenderer>();
    }

    public void ChangeMaterial() => _renderer.sharedMaterial = _overrideMaterial; // Для одного материала
}
