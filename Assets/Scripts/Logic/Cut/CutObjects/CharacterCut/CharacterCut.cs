using DynamicMeshCutter;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterCut : MonoBehaviour, ICuttable, ICutSoundable, ICutUpdatable
{
    [SerializeField] private MeshTarget _target;

    private CutValidator _validator;

    private bool _isActivated;
    private bool _isWork;

    [Inject]
    private IGameplaySoundContainer _soundContainer;

    private void OnValidate()
    {
        _target ??= GetComponentInChildren<MeshTarget>();
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _validator = enemy.Validator;

        MeshTargetEnableToggle(false);
    }

    public void UpdateTargets()
    {
        if (_isWork == false)
            return;

        if (_validator.IsCanCut() == false)
        {
            _isActivated = false;
            MeshTargetEnableToggle(false);
            return;
        }
        else
        {
            _isActivated = true;
            MeshTargetEnableToggle(true);
        }
    }

    public void TryActivateCut() => _isWork = true;

    public void DeactivateCut()
    {
        if (_isActivated == false)
        {
            _validator.HandleFailCut();
            return;
        }

        MeshTargetEnableToggle(false);
        PlaySound();
        _isActivated = false;
        _isWork = false;
    }

    public void PlaySound()
    {
        _soundContainer.Stop();
        _soundContainer.Play(SoundsName.AttackFleshImpact);
    }

    private void MeshTargetEnableToggle(bool isOn)
    {
        if (_target != null)
            _target.enabled = isOn;
    }
}