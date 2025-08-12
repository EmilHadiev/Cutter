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
            // 1. ��������� ��������� ������ �� ����� �������������
            var prefab = await _addressablesLoaderService.LoadAssetAsync(assetPath);

            // 2. ��� ���� ����, ����� ��� ����������� �������� Unity �����������
            await UniTask.NextFrame();

            // 3. ������������ ����� Zenject � ���������� ���������
            var instance = _instantiator.InstantiatePrefab(
                prefab,
                parent?.position ?? Vector3.zero,
                parent?.rotation ?? Quaternion.identity,
                parent
            );

            // 4. ��������� ���������
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
                // �������� "�����" ��������� �� �����������
                r.material = new Material(Shader.Find("Standard"));
            }
        }
    }
}