using UnityEngine;
using Zenject;

public class BonusSpawner : MonoBehaviour, IBonusSpawner
{
    [SerializeField] private Bonus[] _bonus;

    [Inject]
    private readonly IFactory _factory;

    public void Spawn(BonusType type, Vector3 position)
    {
        var prefab = _factory.Create(GetBonus(type).gameObject);
        prefab.transform.position = position;
    }

    private Bonus GetBonus(BonusType bonusType)
    {
        for (int i = 0; i < _bonus.Length; i++)
        {
            if (_bonus[i].BonusType == bonusType)
                return _bonus[i];
        }

        Debug.LogError($"{nameof(bonusType)}");
        return null;
    } 
}