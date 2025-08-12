using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class Factory : MonoBehaviour
{    
    private IAddressablesLoaderService _addressablesLoaderService;
    private IInstantiator _instantiator;

    [Inject]
    private void Constructor(IAddressablesLoaderService addressables, IInstantiator instantiator)
    {
        _addressablesLoaderService = addressables;
        _instantiator = instantiator;
    }

    private void Start()
    {
        Create("Orc").Forget();
    }

    public async UniTask<GameObject> Create(string assetPath, Transform parent = null)
    {
        try
        {
            // 1. Полностью загружаем префаб со всеми зависимостями
            var prefab = await _addressablesLoaderService.LoadAssetAsync(assetPath);

            // 2. Ждём один кадр, чтобы все асинхронные операции Unity завершились
            await UniTask.NextFrame();

            // 3. Инстанцируем через Zenject с правильным родителем
            var instance = _instantiator.InstantiatePrefab(
                prefab,
                parent?.position ?? Vector3.zero,
                parent?.rotation ?? Quaternion.identity,
                parent
            );

            // 4. Проверяем материалы
            FixMaterials(instance);

            return instance;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Factory failed to create {assetPath}: {ex.Message}");
            throw;
        }
    }

    private void FixMaterials(GameObject instance)
    {
        var renderers = instance.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            if (r.material.shader.name.Contains("Error"))
            {
                // Заменяем "битые" материалы на стандартный
                r.material = new Material(Shader.Find("Standard"));
            }
        }
    }
}