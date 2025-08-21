using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AssetReference _refrence;

    private List<GameObject> loadedAssets = new List<GameObject>();
    private IFactory _factory;
    private IAddressablesLoader _loader;

    [Inject]
    private void Constructor(IFactory factory, IAddressablesLoader loader)
    {
        _factory = factory;
        _loader = loader;
    }

    private async UniTask Start()
    {
        try
        {
            var enemies = await _factory.CreateByLabel(AssetProvider.EnemyLabel);
            Debug.Log(enemies.Count);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }        
    }
}