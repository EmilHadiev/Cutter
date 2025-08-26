public interface IEnemy
{
    public EnemyData Data { get; }
    public ICuttable CharacterCut { get; }
    public IHealth Health { get; }
    public IMovable Movable { get; }
    public IEnemyStateMachine StateMachine { get; }
    public IEnemyAnimator Animator { get; }
    public IAttackable Attackable { get; }

    void SetData(EnemyData data);
}