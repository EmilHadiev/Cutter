using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IFactory
{
    UniTask<IList<GameObject>> CreateByLabel(string label);
    UniTask<GameObject> Create(string assetName);
    UniTask<GameObject> Create(string assetName, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    UniTask<GameObject> Create(AssetReference reference);
    UniTask<GameObject> Create(AssetReference reference, Vector3 position = default, Quaternion rotation = default, Transform parent = null);

    /// <summary>
    /// Release the asset
    /// </summary>
    void ReleaseAsset(string assetName);
    void ReleaseAsset(AssetReference reference);
}