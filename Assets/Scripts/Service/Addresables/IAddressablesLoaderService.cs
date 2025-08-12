using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAddressablesLoaderService
{
    UniTask<GameObject> LoadAssetAsync(string assetPath);
    void Release(string assetPath);
    void ReleaseAll();
}