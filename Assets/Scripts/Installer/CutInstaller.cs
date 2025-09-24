using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class CutInstaller : MonoInstaller
{
    [SerializeField] private CustomMouseBehaviour _mouse;

    public override void InstallBindings()
    {
        BindCutCounter();
        BindCutView();
        BindCharacterCutLogic();
        BindCustomMouseBehavior();
        BindCutInstaller();
        BindCutPartExplosionInstaller();
        BindMousePosition();
    }

    private void BindCutCounter()
    {
        Container.BindInterfacesAndSelfTo<CutTargetsCounter>().AsSingle();
    }

    private void BindMousePosition()
    {
        Container.BindInterfacesTo<MousePosition>().AsSingle();
    }

    private void BindCutView()
    {
        Container.BindInterfacesTo<CutView>().AsSingle().NonLazy();
    }

    private void BindCharacterCutLogic()
    {
        Container.BindInterfacesTo<CharacterCutLogic>().AsSingle();
        Container.BindInterfacesTo<ObstacleCutLogic>().AsSingle();
        Container.BindInterfacesTo<ProjectileCutLogic>().AsSingle();
        Container.BindInterfacesTo<TrapCutLogic>().AsSingle();
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