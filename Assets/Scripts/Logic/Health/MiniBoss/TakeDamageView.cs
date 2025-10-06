using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TakeDamageView : MonoBehaviour
{
    [SerializeField] private ParticlePosition _position;

    private IHealth _health;
    private ParticleView _view;

    [Inject] private readonly IGameplaySoundContainer _soundContainer;
    [Inject] private readonly IFactory _factory;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        Create().Forget();
    }

    private void OnEnable() => _health.HealthChanged += OnHealthChanged;

    private void OnDisable() => _health.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged(int health)
    {
        _view.Play();
        _soundContainer.Play(SoundsName.AttackFleshImpact);
    }

    private async UniTaskVoid Create()
    {
        Transform pos = _position.transform;
        var prefab = await _factory.CreateAsync(AssetProvider.DefenseParticle, pos.position, pos.rotation, pos);
        _view = prefab.GetComponent<ParticleView>();
        _view.Stop();
    }
}