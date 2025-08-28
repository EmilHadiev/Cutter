public interface IEnemy
{
    public EnemyData Data { get; }
    public ICuttable CharacterCut { get; }
    public IHealth Health { get; }
    public IMovable Mover { get; }
    public IEnemyStateMachine StateMachine { get; }
    public IEnemyAnimator Animator { get; }
    public IAttackable Attacker { get; }
    public IDefensible Defender { get; }

    void SetData(EnemyData data);
}