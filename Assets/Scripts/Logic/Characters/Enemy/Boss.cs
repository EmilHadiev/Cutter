using UnityEngine;

[RequireComponent(typeof(BossHealth))]
[RequireComponent(typeof(BossAnimator))]
[RequireComponent(typeof(BossAttacker))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Boss : MonoBehaviour
{
    [SerializeField] private BossHealth _health;
    [SerializeField] private BossAnimator _animator;
    [SerializeField] private BossAttacker _attacker;

    public IHealth Health => _health;
    public IBossAnimator Animator => _animator;

    private void OnValidate()
    {
        _health ??= GetComponent<BossHealth>();
        _animator ??= GetComponent<BossAnimator>();
        _attacker ??= GetComponent<BossAttacker>();
    }
}