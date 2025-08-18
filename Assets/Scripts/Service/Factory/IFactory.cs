using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IFactory
{
    UniTask<GameObject> Create(string assetName);
    UniTask<GameObject> Create(string assetName, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    /// <summary>
    /// Release the asset
    /// </summary>
    void ReleaseAsset(string assetName);
}