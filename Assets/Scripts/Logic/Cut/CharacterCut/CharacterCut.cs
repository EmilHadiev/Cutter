using DynamicMeshCutter;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CharacterCut : MonoBehaviour, ICharacterCut
{
    [SerializeField] private MeshTarget _target;

    private void OnValidate() => _target ??= GetComponentInChildren<MeshTarget>();

    private void Awake() => DeactivateCut();

    public void ActivateCut() => _target.enabled = true;

    public void DeactivateCut() => _target.enabled = false;
}