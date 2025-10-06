using UnityEngine;
using Zenject;

public class TicTacToeSpawnTrigger : MonoBehaviour
{
    [Inject] private readonly ITicTacToeSpawner _spawner;

    private void Start()
    {
        _spawner.Spawn(transform.position, transform.rotation);
    }
}