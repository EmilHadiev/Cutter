using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Inject]
    private readonly IFactory _factoryService;

    private async UniTask Start()
    {
        var orc = await _factoryService.Create(AssetProvider.Orc);
        await UniTask.NextFrame();
        var skeleton = await _factoryService.Create(AssetProvider.Skeleton);
        await UniTask.NextFrame();
    }
}