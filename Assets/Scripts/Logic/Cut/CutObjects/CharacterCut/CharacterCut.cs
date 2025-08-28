using DynamicMeshCutter;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterCut : MonoBehaviour, ICuttable
{
    [SerializeField] private MeshTarget _target;

    private IHealth _health;
    private IDefensible _defenser;
    private bool _isActivated;

    private void OnValidate()
    {
        _target ??= GetComponentInChildren<MeshTarget>();
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _health = enemy.Health;
        _defenser = enemy.Defender;

        DeactivateCut();
    }

    public void TryActivateCut()
    {
        if (IsAllowedCut() == false)
            return;

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

    private bool IsAllowedCut()
    {
        if (_defenser.TryDefend())
            return false;

        return true;
    }
}