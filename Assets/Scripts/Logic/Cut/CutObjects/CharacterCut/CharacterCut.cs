using DynamicMeshCutter;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterCut : MonoBehaviour, ICuttable
{
    [SerializeField] private MeshTarget _target;

    private IHealth _health;
    private bool _isActivated;

    private void OnValidate()
    {
        _target ??= GetComponentInChildren<MeshTarget>();
    }

    private void Awake()
    {
        _health = GetComponent<Health>();
        DeactivateCut();
    }

    public void ActivateCut()
    {
        _target.enabled = true;
        _isActivated = true;
    }

    public void DeactivateCut()
    {
        _target.enabled = false;
        TryToKill();
    }

    private void TryToKill()
    {
        if (_isActivated)
        {
            _health.Kill();
            _isActivated = false;
        }
    }
}