using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class CutInstaller : MonoInstaller
{
    [SerializeField] private CustomMouseBehaviour _mouse;

    public override void InstallBindings()
    {
        BindCharacterCutLogic();
        BindCustomMouseBehavior();
        BindCutInstaller();
        BindCutPartExplosionInstaller();
    }

    private void BindCharacterCutLogic()
    {
        Container.BindInterfacesTo<CharacterCutLogic>().AsSingle();
    }

    private void BindCustomMouseBehavior()
    {
        Container.BindInterfacesTo<CustomMouseBehaviour>().FromInstance(_mouse).AsSingle();
    }

    private void BindCutPartExplosionInstaller()
    {
        Container.BindInterfacesTo<CutPartExplosion>().AsSingle().NonLazy();
    }

    private void BindCutInstaller()
    {
        Container.BindInterfacesTo<CutPartContainer>().AsSingle();
    }
}