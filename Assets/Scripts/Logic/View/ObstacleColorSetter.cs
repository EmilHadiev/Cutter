using DynamicMeshCutter;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshTargetMaterialChanger))]
[RequireComponent(typeof(MaterialColorChanger))]
public class ObstacleColorSetter : MonoBehaviour
{
    [SerializeField] private MaterialColorChanger _colorChanger;
    [SerializeField] private MeshTargetMaterialChanger _materialChanger;

    [Inject]
    private readonly ICutMouseBehaviour _mouse;

    private void OnValidate()
    {
        _colorChanger ??= GetComponent<MaterialColorChanger>();
        _materialChanger ??= GetComponent<MeshTargetMaterialChanger>();
    }

    private void OnEnable() => _mouse.CutEnded += _materialChanger.ChangeMaterial;

    private void OnDisable() => _mouse.CutEnded -= _materialChanger.ChangeMaterial;

    public void SetColors(Color[] colors) => _colorChanger.SetColors(colors);
}