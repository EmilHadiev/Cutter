using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class AddressablesSceneLoader : IDisposable, ISceneLoader
{
    private readonly ConcurrentDictionary<int, string> _scenes;
    private readonly ConcurrentDictionary<string, AsyncOperationHandle<SceneInstance>> _activeSceneHandles;

    private CancellationTokenSource _cts;

    public AddressablesSceneLoader()
    {
        _scenes = new ConcurrentDictionary<int, string>();
        _activeSceneHandles = new ConcurrentDictionary<string, AsyncOperationHandle<SceneInstance>>();

        _scenes.TryAdd(0, AssetProvider.Scenes.MainArena.ToString());
        _scenes.TryAdd(1, AssetProvider.Scenes.ViewSelector.ToString());
    }

    public void Restart()
    {
        CancelActiveLoad();

        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (_scenes.TryGetValue(currentBuildIndex, out string sceneName))
            LoadSceneInternal(sceneName, LoadSceneMode.Single).Forget();
        else
            Debug.LogError($"Scene with build index {currentBuildIndex} not found");
    }

    public void SwitchTo(int buildIndex)
    {
        CancelActiveLoad();

        if (_scenes.TryGetValue(buildIndex, out string sceneName))
            LoadSceneInternal(sceneName, LoadSceneMode.Single).Forget();
        else
            Debug.LogError($"Scene with build index {buildIndex} not found");
    }

    public void SwitchTo(string sceneName)
    {
        CancelActiveLoad();

        foreach (var scene in _scenes)
        {
            if (scene.Value == sceneName)
            {
                LoadSceneInternal(sceneName, LoadSceneMode.Single).Forget();
                return;
            }
        }

        Debug.LogError($"Scene with name {sceneName} not found");            
    }

    private async UniTaskVoid LoadSceneInternal(string sceneAddress, LoadSceneMode loadMode)
    {
        _cts = new CancellationTokenSource();

        try
        {
            // Загружаем сцену через Addressables
            AsyncOperationHandle<SceneInstance> sceneHandle = Addressables.LoadSceneAsync(
                sceneAddress,
                loadMode,
                activateOnLoad: true
            );

            // Сохраняем хэндл для последующей выгрузки
            _activeSceneHandles[sceneAddress] = sceneHandle;

            // Ждем завершения загрузки с возможностью отмены
            await sceneHandle.ToUniTask(cancellationToken: _cts.Token);

            if (sceneHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Scene {sceneAddress} loaded successfully!");
            }
            else if (sceneHandle.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogError($"Failed to load scene {sceneAddress}: {sceneHandle.OperationException}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading scene {sceneAddress}: {ex.Message}");
        }
        finally
        {
            _cts?.Dispose();
            _cts = null;
        }
    }

    private void CancelActiveLoad()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = null;
    }

    public void Dispose()
    {
        CancelActiveLoad();

        foreach (var sceneHandle in _activeSceneHandles.Values)
        {
            if (sceneHandle.IsValid())
            {
                Addressables.UnloadSceneAsync(sceneHandle).ToUniTask().Forget();
            }
        }

        _activeSceneHandles.Clear();
    }
}