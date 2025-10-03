using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class SlowMotion : IDisposable, ISlowMotion
{
    private const int Duration = 200;

    private const float SlowTime = 0.5f;
    private const int DefaultTime = 1;

    private CancellationTokenSource _cts;

    public void SlowDownTime() => SlowDown().Forget();

    private async UniTaskVoid SlowDown()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        Debug.Log("Замедляю");
        SetTime(SlowTime);
        await UniTask.Delay(Duration, cancellationToken: _cts.Token);
        SetTime(DefaultTime);
        Debug.Log("Возвращаю!");
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        SetTime(DefaultTime);
    }

    private void SetTime(float time)
    {
        Time.timeScale = time;
        Debug.Log(Time.timeScale);
    }
}