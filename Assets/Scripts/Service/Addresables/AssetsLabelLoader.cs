using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetsLabelLoader
{
    private readonly ConcurrentDictionary<string, (IList<UnityEngine.Object>, AsyncOperationHandle)> _cache;

    private readonly CancellationTokenSource _cts;

    public AssetsLabelLoader(CancellationTokenSource cts)
    {
        _cts = cts;
        _cache = new ConcurrentDictionary<string, (IList<UnityEngine.Object>, AsyncOperationHandle)>();
    }

    /// <summary>
    /// Загружает все ассеты указанного типа по лейблу
    /// </summary>
    /// <typeparam name="T">Тип ассета (GameObject, AudioClip и т.д.)</typeparam>
    /// <param name="label">Лейбл для загрузки</param>
    /// <param name="ct">Токен отмены</param>
    /// <returns>Список загруженных ассетов</returns>
    public async UniTask<IList<T>> LoadAssetsAsync<T>(string label) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(label))
            throw new ArgumentNullException(nameof(label));

        // Проверяем кеш
        if (_cache.TryGetValue(label, out var cached))
        {
            return FilterAssets<T>(cached.Item1);
        }

        try
        {
            // Загружаем ассеты по лейблу
            var handle = Addressables.LoadAssetsAsync<UnityEngine.Object>(label, null);
            await handle.ToUniTask(cancellationToken: _cts.Token);

            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load assets for label: {label}");

            // Кешируем результат
            var assets = handle.Result;
            _cache[label] = (assets, handle);

            return FilterAssets<T>(assets);
        }
        catch (OperationCanceledException)
        {
            if (_cache.TryRemove(label, out var entry))
                Addressables.Release(entry.Item2);
            throw;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[AssetLoader] Error loading '{label}': {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Фильтрует ассеты по указанному типу
    /// </summary>
    private IList<T> FilterAssets<T>(IList<UnityEngine.Object> assets) where T : UnityEngine.Object
    {
        var result = new List<T>();
        foreach (var asset in assets)
        {
            if (asset is T typedAsset)
                result.Add(typedAsset);
        }
        return result;
    }

    /// <summary>
    /// Освобождает ассеты для указанного лейбла
    /// </summary>
    public bool TryReleaseAssets(string label)
    {
        if (_cache.TryRemove(label, out var entry))
        {
            Addressables.Release(entry.Item2);
            return true;
        }

        return false;
    }

    public void ReleaseAssets()
    {
        foreach (var entry in _cache)
        {
            Addressables.Release(entry.Value.Item2);
        }
        _cache.Clear();
    }
}