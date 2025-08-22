using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class CutInstaller : MonoInstaller
{
    [SerializeField] private CustomMouseBehaviour _mouse;

    public override void InstallBindings()
    {
        BindCutImpact();
        BindCutView();
        BindCharacterCutLogic();
        BindCustomMouseBehavior();
        BindCutInstaller();
        BindCutPartExplosionInstaller();
        BindMousePosition();
    }

    private void BindMousePosition()
    {
        Container.BindInterfacesTo<MousePosition>().AsSingle();
    }

    private void BindCutImpact()
    {
        Container.BindInterfacesTo<CutImpact>().AsSingle().NonLazy();
    }

    private void BindCutView()
    {
        Container.BindInterfacesTo<CutView>().AsSingle().NonLazy();
    }

    private void BindCharacterCutLogic()
    {
        Container.BindInterfacesTo<CharacterCutLogic>().AsSingle();
        //Container.BindInterfacesTo<ObstacleCutLogic>().AsSingle().NonLazy();
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