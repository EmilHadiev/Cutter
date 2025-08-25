public interface IEnemy
{
    public EnemyData Data { get; }
    public ICuttable CharacterCut { get; }
    public IHealth Health { get; }
    public IMovable Movable { get; }

    void SetData(EnemyData data);
}