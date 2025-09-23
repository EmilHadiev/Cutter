using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(ProjectileMover))]
[RequireComponent(typeof(ProjectileCut))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private ProjectileMover _mover;
    [SerializeField] private ProjectileCut _cut;

    private const int Damage = 1;

    private IMovable _player;
    private IFactory _factory;
    private ProjectileView _view;

    private Transform _sender;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
        _mover ??= GetComponent<ProjectileMover>();
        _cut ??= GetComponent<ProjectileCut>();
    }

    private async void Awake()
    {
        var particle = await GetParticle();
        _view = new ProjectileView(particle, Color.red, transform);
    }

    private async UniTask<ParticleView> GetParticle()
    {
        var prefab = await _factory.CreateAsync(AssetProvider.Particles.FireParticle.ToString());
        prefab.gameObject.transform.parent = transform;
        prefab.transform.localPosition = Vector3.zero;
        var particle = prefab.GetComponent<ParticleView>();
        return particle;
    }

    private void OnEnable()
    {
        _observer.Entered += Hit;
        _cut.Cut += OnCut;
    }

    private void OnDisable()
    {
        _observer.Entered -= Hit;
        _cut.Cut -= OnCut;
    }

    [Inject]
    private void Constructor(IPlayer player, IFactory factory)
    {
        _player = player.Movable;
        _factory = factory;
    }

    public void StartMove(Transform sender)
    {
        _sender = sender;
        _mover.SetTarget(_player.Transform);

        _view?.Show();
    }

    private void Hit(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
            health.TakeDamage(Damage);

        gameObject.SetActive(false);
    }

    private void OnCut() => _mover.SetTarget(_sender);
}