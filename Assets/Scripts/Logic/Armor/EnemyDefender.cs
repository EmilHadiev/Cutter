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

    private bool _isWorking = true;
    private bool _isDefending;

    public bool IsCanDefend => _shield != null && _shield.gameObject.activeInHierarchy == true && _isWorking;

    private void OnValidate()
    {
        _shield ??= GetComponentInChildren<Shield>();
    }

    private void Awake()
    {
        if (IsCanDefend == false)
        {
            enabled = false;
            _isWorking = false;
        }    
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
        if (IsCanDefend == false)
        {
            Debug.Log("Больше не могу дефать!");
            Debug.Log($"is working {_isWorking}. is defending {_isDefending}");
            return false;
        }

        _isDefending = true;
        return true;
    }

    public void Deactivate()
    {
        _isWorking = false;
    }

    public void Activate()
    {
        _isWorking = true;
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
        if (_isDefending)
        {
            _view.Play();
            _animator.SetDefenseTrigger();
            TryAttackShield();
            _isDefending = false;
        }
    }

    private void TryAttackShield()
    {
        if (_isWorking)
            _shield.TakeDamage();
    }
}