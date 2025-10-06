using UnityEngine;
using Zenject;

public class TicTacToeSpawner : MonoBehaviour, ITicTacToeSpawner
{
    [SerializeField] private TicTacToe _template;

    [Inject] private readonly IFactory _factory;

    public void Spawn(Vector3 position, Quaternion quaternion)
    {
        var prefab = _factory.Create(_template.gameObject);
        prefab.transform.position = position;
        prefab.transform.rotation = quaternion;
    }
}