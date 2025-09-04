public interface IEnemy
{
    public EnemyData Data { get; }
    public IHealth Health { get; }
    public IMovable Mover { get; }
    public IEnemyStateMachine StateMachine { get; }
    public IEnemyAnimator Animator { get; }
    public IAttackable Attacker { get; }
    public IDefensible Defender { get; }
    public IParryable Parryer { get; }
    public CutValidator Validator { get; }

    void SetData(EnemyData data);
}