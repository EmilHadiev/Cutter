using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
            await LoadAssetsByLabel("Enemy");

            await UniTask.NextFrame();
            var skeleton = await _factory.Create(AssetProvider.Skeleton);
            await UniTask.NextFrame();
            var orc2 = await _factory.Create(AssetProvider.Orc);
            await UniTask.NextFrame();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }        
    }

    private async UniTask LoadAssetsByLabel(string label)
    {
        await Addressables.LoadAssetsAsync<GameObject>(
            label,
            loadedAsset =>
            {
                // Этот коллбэк вызывается для каждого загруженного ассета
                Debug.Log($"Загружен ассет: {loadedAsset.name}");
                loadedAssets.Add(loadedAsset);

                // Например, создаём объект на сцене
                Instantiate(loadedAsset, transform);
            }
        ).ToUniTask();
    }

    private void OnDestroy()
    {
        // Освобождаем ресурсы (если нужно)
        foreach (var asset in loadedAssets)
        {
            Addressables.Release(asset);
        }
    }
}