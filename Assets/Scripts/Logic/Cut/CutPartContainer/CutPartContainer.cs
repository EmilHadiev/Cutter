using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CutPartContainer : ICutPartContainer, IDisposable
{
    private const int HideDelay = 2500;
    private const int DestroyObjectInterval = 3000;
    private const int MaxInactiveObjects = 20;

    private readonly Queue<GameObject> _inactiveObjects = new Queue<GameObject>();

    private CancellationTokenSource _cts;

    public event Action<GameObject> Added;

    public CutPartContainer()
    {
        _cts = new CancellationTokenSource();
        ClearObjectsLoop().Forget();
    }

    public void Add(GameObject cutPart)
    {
        if (cutPart == null || !cutPart.activeSelf)
            return;

        Added?.Invoke(cutPart);
        ProcessCutPart(cutPart).Forget();
    }

    private async UniTaskVoid ProcessCutPart(GameObject cutPart)
    {
        // Шаг 1: Скрываем через 1 секунду
        try
        {
            await UniTask.Delay(HideDelay, cancellationToken: _cts.Token);

            if (cutPart == null) 
                return;

            cutPart.SetActive(false);
            _inactiveObjects.Enqueue(cutPart);

            // Оптимизация: отключаем физику
            if (cutPart.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.isKinematic = true;
            }
        }
        catch (OperationCanceledException) { }
    }

    private async UniTaskVoid ClearObjectsLoop()
    {
        while (!_cts.IsCancellationRequested)
        {
            await UniTask.Delay(DestroyObjectInterval, cancellationToken: _cts.Token);

            // Уничтожаем старые объекты, если превышен лимит
            while (_inactiveObjects.Count > MaxInactiveObjects)
            {
                var oldest = _inactiveObjects.Dequeue();
                if (oldest != null)
                {
                    GameObject.Destroy(oldest);
                }
            }
        }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _cts?.Dispose();

        // Уничтожаем все оставшиеся объекты
        while (_inactiveObjects.Count > 0)
        {
            var obj = _inactiveObjects.Dequeue();
            if (obj != null) 
                GameObject.Destroy(obj);
        }
    }
}