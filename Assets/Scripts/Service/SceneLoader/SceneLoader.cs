using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    private CancellationTokenSource _cts;

    public void SwitchTo(int buildIndex)
    {
        CancelActiveLoad();
        LoadSceneInternal(() =>
            SceneManager.LoadSceneAsync(
                buildIndex,
                LoadSceneMode.Single))
            .Forget(); //для void
    }

    public void SwitchTo(string sceneName)
    {
        CancelActiveLoad();
        LoadSceneInternal(() =>
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single))
            .Forget();
    }

    public void Restart()
    {
        CancelActiveLoad();
        LoadSceneInternal(() =>
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single))
            .Forget();
    }

    private async UniTaskVoid LoadSceneInternal(Func<AsyncOperation> loadOperation)
    {
        _cts = new CancellationTokenSource();

        try
        {
            AsyncOperation asyncOp = loadOperation();
            asyncOp.allowSceneActivation = true; // Важно для WebGL

            await asyncOp.WithCancellation(_cts.Token);

            if (asyncOp.isDone)
            {
                //например для сейва игры
                Debug.Log("Scene loaded successfully!");
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Scene loading was cancelled");
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
}