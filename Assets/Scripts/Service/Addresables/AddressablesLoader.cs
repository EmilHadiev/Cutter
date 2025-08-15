using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesLoader : IAddressablesLoader, IDisposable
{
    private readonly CancellationTokenSource _cts;
    private readonly ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>> _assets;

    public AddressablesLoader()
    {
        Addressables.InitializeAsync().ToUniTask().Forget();
        _cts = new CancellationTokenSource();
        _assets = new ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>>();
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

            throw; // Пробрасываем оригинальное исключение
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset: {assetPath}. Error: {ex.Message}");
            throw; // Пробрасываем оригинальное исключение
        }
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