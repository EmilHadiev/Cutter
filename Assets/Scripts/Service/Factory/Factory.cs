using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Factory : IFactory
{
    private IAddressablesLoader _addressablesLoaderService;
    private IInstantiator _instantiator;

    [Inject]
    private void Constructor(IAddressablesLoader addressables, IInstantiator instantiator)
    {
        _addressablesLoaderService = addressables;
        _instantiator = instantiator;
    }

    public async UniTask<GameObject> Create(string assetName, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        var prefab = await _addressablesLoaderService.LoadAssetAsync<GameObject>(assetName);
        return _instantiator.InstantiatePrefab(prefab, position, rotation, parent);
    }

    public async UniTask<GameObject> Create(string assetName)
    {
        var prefab = await _addressablesLoaderService.LoadAssetAsync<GameObject>(assetName);
        return _instantiator.InstantiatePrefab(prefab);
    }

    public void ReleaseAsset(string assetName)
    {
        _addressablesLoaderService.Release(assetName);
    }

    public async UniTask<IList<GameObject>> CreateByLabel(string label)
    {
        IList<GameObject> prefabs = await _addressablesLoaderService.LoadAssetsByLabelAsync<GameObject>(label);
        IList<GameObject> results = new List<GameObject>(prefabs.Count);
        
        foreach (var prefab in prefabs)
        {
            results.Add(_instantiator.InstantiatePrefab(prefab));
        }

        return results;
    }
}