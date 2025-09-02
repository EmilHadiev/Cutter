using DynamicMeshCutter;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ShieldHealth))]
[RequireComponent(typeof(MeshTarget))]
public class Shield : MonoBehaviour, ICutSoundable
{
    [SerializeField] private ShieldHealth _health;
    [SerializeField] private MeshTarget _target;

    [Inject]
    private readonly ISoundContainer _soundContainer;

    private const int Damage = 1;

    private void OnValidate()
    {
        _health ??= GetComponent<ShieldHealth>();
        _target ??= GetComponent<MeshTarget>();
    }

    private void Awake()
    {
        _target.enabled = false;
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void SetHealth(int health)
    {
        _health.SetHealth(health);
    }

    public void TakeDamage()
    {
        _health.TakeDamage(Damage);
        Debug.Log("Урон по щиту получен!");
    }

    public void PlaySound()
    {
        _soundContainer.Stop();
        _soundContainer.Play(SoundsName.ShieldImpact);
    }

    private void OnDied()
    {
        _target.enabled = true;
    }
}