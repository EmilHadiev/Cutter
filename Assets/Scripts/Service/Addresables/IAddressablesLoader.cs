using Cysharp.Threading.Tasks;

public interface IAddressablesLoader
{
    UniTask<T> LoadAssetAsync<T>(string assetPath) where T : UnityEngine.Object;
    void Release(string assetPath);
    void ReleaseAll();
}