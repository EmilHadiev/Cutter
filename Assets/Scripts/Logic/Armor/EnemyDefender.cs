using UnityEngine;
using Zenject;

public class EnemyDefender : MonoBehaviour, IDefensible
{
    [SerializeField] private Shield _shield;

    private IEnemyAnimator _animator;
    private EnemyData _data;
    private DefenderView _view;

    private bool _isWorking;

    public bool IsCanDefending => _shield != null && _shield.gameObject.activeInHierarchy == true && _isWorking;

    public bool IsCanCut
    {
        get
        {
            if (enabled == false)
                return true;

            return IsCanDefending == false;
        }
    }

    private void OnValidate()
    {
        _shield ??= GetComponentInChildren<Shield>();
    }

    private void Awake()
    {
        if (enabled == false)
        {
            _shield.gameObject.SetActive(false);
            return;
        }

        IEnemy enemy = GetComponent<IEnemy>();

        _animator = enemy.Animator;
        _data = enemy.Data;

        _isWorking = true;

        _shield.SetHealth(_data.ShieldHealth);
    }

    [Inject]
    private void Constructor(IFactory factory)
    {
        _view = new DefenderView(factory, _shield);
    }

    public void Deactivate()
    {
        _isWorking = false;

        if (_shield != null && _shield.gameObject != null)
            _shield.gameObject.SetActive(false);
    }

    public void Activate()
    {
        _isWorking = true;

        if (_shield != null && _shield.gameObject != null)
            _shield.gameObject.SetActive(true);
    }

    public void HandleFailCut()
    {
        TryDefend();
    }

    private void TryDefend()
    {
        if (IsCanDefending)
        {
            _view.Play();
            _animator.SetDefenseTrigger();
            _shield.TakeDamage();
        }
    }

    #region FromAnimations

    private void DefenseEnded()
    {
        _animator.ResetDefenseTrigger(); 
    }
    #endregion
}