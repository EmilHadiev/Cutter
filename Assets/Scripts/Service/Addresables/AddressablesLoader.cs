using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesLoader : IAddressablesLoader, IDisposable
{
    private readonly CancellationTokenSource _cts;
    private readonly ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>> _assets;
    private readonly AssetsLabelLoader _labelLoader;

    public AddressablesLoader()
    {
        Addressables.InitializeAsync().ToUniTask().Forget();
        _cts = new CancellationTokenSource();
        _assets = new ConcurrentDictionary<string, AsyncOperationHandle<UnityEngine.Object>>();
        _labelLoader = new AssetsLabelLoader(_cts);
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
                Debug.Log($"Returning an existing asset {assetPath}");
                return await existingHandle.ToUniTask(cancellationToken: _cts.Token) as T;
            }

            var handle = Addressables.LoadAssetAsync<UnityEngine.Object>(assetPath);
            _assets.TryAdd(assetPath, handle);

            Debug.Log($"Load and retrieve the asset {assetPath}");
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
            Debug.Log($"Load by reference {key}");
            return await LoadAssetAsync<T>(key);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load asset. Error: {ex.Message}");
            throw;
        }
    }

    public async UniTask<IList<T>> LoadAssetsByLabelAsync<T>(string label) where T : UnityEngine.Object
    {
        return await _labelLoader.LoadAssetsAsync<T>(label);
    }

    public void Release(string assetPath)
    {
        if (_assets.TryRemove(assetPath, out var handle))
        {
            Addressables.Release(handle);
            return;
        }

        if (_labelLoader.TryReleaseAssets(assetPath))
            return;

        throw new ArgumentException($"Asset '{assetPath}' is not loaded.", nameof(assetPath));
    }

    public void Release(AssetReference reference)
    {
        Release(reference.RuntimeKey.ToString());
    }

    public void ReleaseAll()
    {
        foreach (var asset in _assets.Values)
            Addressables.Release(asset);

        _labelLoader.ReleaseAssets();

        _assets.Clear();
    }
}