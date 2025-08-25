using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindPlayer()
    {
        Container.BindInterfacesTo<Player>().FromInstance(_player).AsSingle();
    }
}