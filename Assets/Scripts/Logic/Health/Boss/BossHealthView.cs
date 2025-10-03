using UnityEngine;
using Zenject;

public class BossHealthView : MonoBehaviour
{
    [SerializeField] private ParticlePosition _position;

    private IFactory _factory;
    private ISlowMotion _slowMotion;

    private ParticleView _view;
    private IHealth _health;
    private IBossAnimator _animator;

    private void Awake()
    {
        Boss boss = GetComponent<Boss>();
        _health = boss.Health;
        _animator = boss.Animator;
    }

    private async void Start()
    {
        var prefab = await _factory.CreateAsync(AssetProvider.DemonExplosionParticle);
        prefab.transform.position = _position.transform.position;

        _view = prefab.GetComponent<ParticleView>();
        _view.Stop();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.Died -= OnDied;
    }

    [Inject]
    private void Constructor(IFactory factory, ISlowMotion slowMotion)
    {
        _factory = factory;
        _slowMotion = slowMotion;
    }

    private void OnHealthChanged(int health)
    {
        _animator.SetGetHitTrigger();
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
        _view.Play();
        _slowMotion.SlowDownTime();
    }
}