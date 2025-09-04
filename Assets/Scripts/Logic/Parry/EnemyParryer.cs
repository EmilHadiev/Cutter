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
    private ISoundContainer _soundContainer;

    private bool _isParryWindowOpen;
    private bool _isWorking;

    private bool IsCanParry => _isParryWindowOpen == true && _isWorking == true;

    public bool IsCanCut => IsCanParry == false;

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
    private void Constructor(ISoundContainer soundContainer, IFactory factory)
    {
        _factory = factory;
        _soundContainer = soundContainer;
    }

    public void Activate() => _isWorking = true;
    public void Deactivate() => _isWorking = false;

    private void OpenParryWindow()
    {
        _parryView.ShowParryWindow();
        _isParryWindowOpen = true;
    }

    private void CloseParryWindow()
    {
        _parryView.CloseParryWindow();
        _isParryWindowOpen = false;
    }

    private void TryParry()
    {
        if (IsCanParry == false)
        {
            return;
        }

        _parryView.ShowParryImpact();
    }

    public void HandleFailCut()
    {
        TryParry();
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
        _defender.Deactivate();
        CloseParryWindow();
    }

    private void StunEnded()
    {
        _defender.Activate();
        _parryView.CloseParryImpact();
    }

    #endregion
}