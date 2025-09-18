using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IFactory
{
    GameObject Create(GameObject prefab);
    GameObject Create(GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    UniTask<IList<GameObject>> CreateAsyncByLabel(string label);
    UniTask<GameObject> CreateAsync(string assetName);
    UniTask<GameObject> CreateAsync(string assetName, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    UniTask<GameObject> CreateAsync(AssetReference reference);
    UniTask<GameObject> CreateAsync(AssetReference reference, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    /// <summary>
    /// Release the asset
    /// </summary>
    void ReleaseAsset(string assetName);
    void ReleaseAsset(AssetReference reference);
}