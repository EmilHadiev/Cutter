using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class EnemyDefender : MonoBehaviour, IDefensible
{
    [SerializeField] private Shield _shield;

    private IEnemyAnimator _animator;
    private EnemyData _data;
    private DefenderView _view;
    private ICutMouseBehaviour _mouseBehaviour;
    private IParryable _parryer;

    public bool IsDefending { get; private set; }

    public bool IsShieldExisting => _shield != null && _shield.gameObject.activeInHierarchy;

    private void OnValidate()
    {
        _shield ??= GetComponentInChildren<Shield>();
    }

    private void Awake()
    {
        if (IsShieldExisting == false)
            enabled = false;
    }

    private void OnEnable()
    {
        _mouseBehaviour.CutEnded += OnCutEnded;
    }

    private void OnDisable()
    {
        _mouseBehaviour.CutEnded -= OnCutEnded;
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _animator = enemy.Animator;
        _data = enemy.Data;
        _parryer = enemy.Parryer;

        _shield.SetHealth(_data.ShieldHealth);
    }

    [Inject]
    private void Constructor(ICutMouseBehaviour mouseBehaviour, IFactory factory, ISoundContainer soundContainer)
    {
        _mouseBehaviour = mouseBehaviour;
        _view = new DefenderView(factory, _shield);
    }

    public bool TryDefend()
    {
        if (enabled == false)
        {
            return false;
        }

        if (IsShieldExisting == false)
        {
            _parryer.Deactivate();
            return false;
        }
        else
        {
            _parryer.Activate();
        }

        if (_parryer.IsParryTime)
        {
            return true;
        }

        _shield.TakeDamage();
        StartDefend();
        return true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    public void Activate()
    {
        enabled = true;
    }

    private void StartDefend()
    {
        IsDefending = true;        
    }

    /// <summary>
    /// From animation
    /// </summary>
    private void DefenseEnded()
    {
        _animator.ResetDefenseTrigger(); 
    }

    private void OnCutEnded()
    {
        if (IsDefending)
        {
            PlayView();
            _animator.SetDefenseTrigger();
            IsDefending = false;
        }
    }

    private void PlayView()
    {
        _view.Play();
    }
}