using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour, IEnemyAnimator
{
    [SerializeField] private Animator _animator;

    private const string IsRunning = "IsRunning";
    private const string IsAttacking1 = "IsAttacking1";
    private const string IsAttacking2 = "IsAttacking2";

    private void OnValidate()
    {
        _animator ??= GetComponent<Animator>();
    }

    public void PlayRunning() => _animator.SetBool(IsRunning, true);
    public void StopRunning() => _animator.SetBool(IsRunning, false);

    public void PlayAttacking1() => _animator.SetBool(IsAttacking1, true);
    public void StopAttacking1() => _animator.SetBool(IsAttacking1, false);

    public void PlayAttacking2() => _animator.SetBool(IsAttacking2, true);
    public void StopAttacking2() => _animator.SetBool(IsAttacking2, false);

}