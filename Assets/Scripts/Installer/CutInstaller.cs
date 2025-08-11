using Zenject;

public class CutInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCutInstaller();
    }

    private void BindCutInstaller()
    {
        Container.BindInterfacesTo<CutPartContainer>().AsSingle();
    }
}