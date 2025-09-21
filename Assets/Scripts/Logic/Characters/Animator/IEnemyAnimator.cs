public interface IEnemyAnimator
{
    void PlayAttacking1();
    void PlayAttacking2();
    void PlayRunning();
    void StopAttacking1();
    void StopAttacking2();
    void StopRunning();

    void SetVictoryTrigger();
    void StopVictory();

    void SetDefenseTrigger();
    void ResetDefenseTrigger();

    void SetStunTrigger();
    void ResetStunTrigger();

    void SetAttackSpeed(float speed = 1);
    void ResetAttackSpeed();
}