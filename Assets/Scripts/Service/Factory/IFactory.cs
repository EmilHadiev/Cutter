using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IFactory
{
    UniTask<GameObject> Create(string assetName, Vector3 position = default, Quaternion rotation = default, Transform parent = null);
    void ReleaseAsset(string assetName);
}