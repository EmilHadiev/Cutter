using UnityEngine;
using Zenject;

public class PlayerSpawnPlace : MonoBehaviour
{
    private IPlayer _player;
    private IGameStarter _starter;
    private GameObject _playerEnable;

    private void Awake()
    {
        _playerEnable = _player.Movable.Transform.gameObject;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    [Inject]
    private void Constructor(IPlayer player, IGameStarter starter)
    {
        _player = player;
        _starter = starter;
    }
}
