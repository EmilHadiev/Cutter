public interface IEnemyAnimator
{
    void PlayAttacking1();
    void PlayAttacking2();
    void PlayRunning();
    void StopAttacking1();
    void StopAttacking2();
    void StopRunning();

    void SetDefenseTrigger();
    void ResetDefenseTrigger();

    void SetStunTrigger();
    void ResetStunTrigger();

    void SetVictoryTrigger();

    void SetAttackSpeed(float speed = 1);
    void ResetAttackSpeed();
}