using Cysharp.Threading.Tasks;

public interface IAddresablesSceneLoader : ISceneLoader
{
    UniTask UnloadSceneAsync(string sceneAddress);
}