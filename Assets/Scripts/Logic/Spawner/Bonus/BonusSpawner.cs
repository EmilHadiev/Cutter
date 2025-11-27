using UnityEngine;
using Zenject;

public class BonusSpawner : MonoBehaviour, IBonusSpawner
{
    [SerializeField] private Bonus[] _bonus;

    private bool _isBonusLevel;

    [Inject]
    private readonly IFactory _factory;
    [Inject]
    private readonly PlayerProgress _progress;

    public void Spawn(BonusType type, Vector3 position)
    {
        var prefab = _factory.Create(GetBonus(type).gameObject);
        prefab.transform.position = position;
    }

    private Bonus GetBonus(BonusType bonusType)
    {
        for (int i = 0; i < _bonus.Length; i++)
        {
            if (IsRandomSpawn())
                return GetRandomBonus();

            if (_bonus[i].BonusType == bonusType)
                return _bonus[i];
        }

        Debug.LogError($"{nameof(bonusType)}");
        return null;
    }

    private bool IsRandomSpawn()
    {
        return _progress.IsHardcoreMode;
    }

    private Bonus GetRandomBonus()
    {
        int randomIndex = Random.Range(0, _bonus.Length);
        return _bonus[randomIndex];
    }
}