using UnityEngine;
using Zenject;

public class EnemyParryer : MonoBehaviour, IParryable
{
    [SerializeField] private ParticlePosition _parryParticlePosition;
    [SerializeField] private ParticlePosition _stunParticlePosition;

    private EnemyParryView _parryView;

    private IEnemy _enemy;
    private IEnemyAnimator _animator;
    private IFactory _factory;
    private IDefensible _defender;
    private IGameplaySoundContainer _soundContainer;

    private bool _isParryWindowOpen;
    private bool _isWorking;

    private bool IsCanParry => _isParryWindowOpen == true && _isWorking == true;

    public bool IsCanCut
    {
        get
        {
            if (gameObject == null)
                return true;

            if (this == null)
                return true;

            if (enabled == false)
                return true;

            return IsCanParry == false;
        }
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        _animator = _enemy.Animator;
        _defender = _enemy.Defender;

        if (_defender.IsCanDefending)
            Activate();

        _parryView = new EnemyParryView(_soundContainer, _factory, _animator, _parryParticlePosition, _stunParticlePosition);
    }

    [Inject]
    private void Constructor(IGameplaySoundContainer soundContainer, IFactory factory)
    {
        _factory = factory;
        _soundContainer = soundContainer;
    }

    public void Activate() => _isWorking = true;
    public void Deactivate() => _isWorking = false;

    public void HandleFailCut() => TryParry();

    private void TryParry()
    {
        if (IsCanParry == false)
        {
            return;
        }

        _parryView.ShowParryImpact();
    }

    private void OpenParryWindow()
    {
        if (enabled == false)
            return;

        _parryView.ShowParryWindow();
        _isParryWindowOpen = true;
    }

    private void CloseParryWindow()
    {
        if (enabled == false)
            return;

        _parryView.CloseParryWindow();
        _isParryWindowOpen = false;
    }

    private void DefenderToggle(bool isOn)
    {
        if (enabled == false)
            return;

        if (isOn)
            _defender.Activate();
        else
            _defender.Deactivate();
    }

    #region FromAnimations

    private void ParryTimeStarted()
    {
        if (_isWorking == false)
            return;

        OpenParryWindow();
    }

    private void ParryTimeEnded()
    {        
        CloseParryWindow();
    }

    private void StunStarted()
    {
        DefenderToggle(false);
        CloseParryWindow();
    }

    private void StunEnded()
    {
        DefenderToggle(true);
        _parryView.CloseParryImpact();
    }

    #endregion
}