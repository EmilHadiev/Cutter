using DynamicMeshCutter;
using UnityEngine;

[RequireComponent(typeof(MeshTarget))]
public class Helmet : MonoBehaviour
{
    [SerializeField] private MeshTarget _mesh;

    private void OnValidate()
    {
        _mesh ??= GetComponent<MeshTarget>();
    }

    public void CutEnable(bool isOn) => _mesh.enabled = isOn;
    public void EnableToggle(bool isOn) => gameObject.SetActive(isOn);
}