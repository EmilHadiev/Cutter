using UnityEngine;

[RequireComponent(typeof(CharacterCut))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private CharacterCut _characterCut;
    [SerializeField] private Health _health;
    [SerializeField] private EnemyMover _mover;

    private EnemyData _data;

    private void OnValidate()
    {
        _characterCut ??= GetComponent<CharacterCut>();
        _health ??= GetComponent<Health>();
        _mover ??= GetComponent<EnemyMover>();
    }

    public void SetData(EnemyData data)
    {
        _data = data;
    }

    public ICuttable CharacterCut => _characterCut;

    public IHealth Health => _health;

    public IMovable Movable => _mover;

    public EnemyData Data => _data;
}