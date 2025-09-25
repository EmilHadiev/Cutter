using UnityEngine;
using Zenject;

public class BonusPlace : MonoBehaviour
{
    [SerializeField] private BonusType _type;

    [Inject]
    private readonly IBonusSpawner _spawner;

    private void Start()
    {
        _spawner.Spawn(_type, transform.position);
    }
}