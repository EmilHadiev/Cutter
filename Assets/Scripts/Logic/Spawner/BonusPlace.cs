using UnityEngine;
using Zenject;

public class BonusPlace : MonoBehaviour
{
    [Inject]
    private readonly IBonusSpawner _spawner;

    private async void Start()
    {
        _spawner.Spawn(transform.position);
    }
}