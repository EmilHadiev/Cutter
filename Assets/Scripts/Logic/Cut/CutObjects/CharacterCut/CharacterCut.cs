using DynamicMeshCutter;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterCut : MonoBehaviour, ICuttable, ICutSoundable, ICutUpdatable
{
    [SerializeField] private MeshTarget _target;

    private CutValidator _validator;
    private ICombatSession _combatSession;
    private IGameplaySoundContainer _soundContainer;

    private bool _isActivated;
    private bool _isWork;

    

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

    [Inject]
    private void Constructor(IGameplaySoundContainer soundContainer, ICombatSession combatSession)
    {
        _soundContainer = soundContainer;
        _combatSession = combatSession;
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

    public void TryActivateCut()
    {
        _isWork = true;
        Debug.Log(gameObject.name);
    }

    public void DeactivateCut()
    {
        if (_isActivated == false)
        {
            _validator.HandleFailCut();
            MeshTargetEnableToggle(false);
            return;
        }

        PlaySound();
        _isActivated = false;
        _isWork = false;
        _combatSession.RemoveEnemy();
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