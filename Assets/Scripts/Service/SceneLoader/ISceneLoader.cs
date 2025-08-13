public interface ISceneLoader
{
    void Restart();
    void SwitchTo(int buildIndex);
    void SwitchTo(string sceneName);
}