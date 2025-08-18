using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesLoader : IAddressablesLoader, IDisposable
{
    private readonly CancellationTokenSource _cts;
    private readonly ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>> _assets;
    private readonly ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>> _labels;

    public AddressablesLoader()
    {
        Addressables.InitializeAsync().ToUniTask().Forget();
        _cts = new CancellationTokenSource();
        _assets = new ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>>();
        _labels = new ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>>();
    }

    public void Dispose()
    {
        ReleaseAll();
        _cts?.Cancel();
        _cts?.Dispose();
    }

    public async UniTask<T> LoadAssetAsync<T>(string assetPath) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(assetPath))
            throw new ArgumentException(nameof(assetPath));

        try
        {
            if (_assets.TryGetValue(assetPath, out var existingHandle))
            {
                Debug.Log("ВОЗВРАЩАЮ УЖЕ ИМЕЮЩИЙСЯ АССЕТ!");
                return await existingHandle.ToUniTask(cancellationToken: _cts.Token) as T;
            }

            var handle = Addressables.LoadAssetAsync<UnityEngine.Object>(assetPath);
            _assets.TryAdd(assetPath, handle);

            return await handle.ToUniTask(cancellationToken: _cts.Token) as T;
        }
        catch (OperationCanceledException)
        {
            if (_assets.TryRemove(assetPath, out var handle))
            {
                Addressables.Release(handle);
            }

            throw;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset: {assetPath}. Error: {ex.Message}");
            throw;
        }
    }

    public async UniTask<T> LoadAssetAsync<T>(AssetReference reference) where T : UnityEngine.Object
    {
        if (reference == null)
            throw new ArgumentNullException(nameof(reference));

        try
        {
            string key = reference.RuntimeKey.ToString();
            return await LoadAssetAsync<T>(key);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset. Error: {ex.Message}");
            throw;
        }
    }

    public async UniTask<IList<T>> LoadAssetsByGroupAsync<T>(string group) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(group))
            throw new ArgumentException(nameof(group));

        try
        {
            // 1. Загружаем список ассетов в группе
            var locations = await Addressables.LoadResourceLocationsAsync(group).ToUniTask(cancellationToken: _cts.Token);

            // 2. (Опционально) Фильтруем по типу T
            var filteredLocations = locations.Where(l => l.ResourceType == typeof(T)).ToList();

            // 3. Загружаем параллельно с кешированием
            var tasks = new List<UniTask<T>>();
            foreach (var location in filteredLocations)
            {
                tasks.Add(LoadAssetAsync<T>(location.PrimaryKey)); // Используем кеширующий метод
            }

            // 4. Ждём завершения всех загрузок
            return (await UniTask.WhenAll(tasks)).ToList();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Загрузка отменена!");
            throw;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Ошибка загрузки группы {group}: {ex.Message}");
            throw;
        }
    }

    public async UniTask<IList<T>> LoadAssetsByLabelAsync<T>(string label) where T : UnityEngine.Object
    {
        throw new NotImplementedException();
    }

    public void Release(string assetPath)
    {
        if (_assets.TryRemove(assetPath, out var handle))
        {
            Addressables.Release(handle);

        }
        else
        {
            throw new ArgumentException($"Asset '{assetPath}' is not loaded.", nameof(assetPath));
        }
    }

    public void ReleaseAll()
    {
        foreach (var asset in _assets.Values)
            Addressables.Release(asset);

        _assets.Clear();
    }
}