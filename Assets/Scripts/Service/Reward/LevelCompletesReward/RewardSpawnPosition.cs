using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class RewardSpawnPosition : MonoBehaviour
{
    [SerializeField] private Vector3 _particleSpawnPosition;
    [SerializeField] private Vector3 _particleScale;

    [Inject] private readonly IFactory _factory;

    private ParticleView _view;

    private void Awake()
    {
        CreateAndPlayParticleView().Forget();
    }

    public void SetReward(SkinData skin)
    {
        
    }

    private async UniTask CreateAndPlayParticleView()
    {
        var prefab = await _factory.CreateAsync(AssetProvider.Particles.LightParticle.ToString());
        prefab.transform.parent = transform;
        prefab.transform.localScale = _particleScale;
        prefab.transform.localPosition = _particleSpawnPosition;

        _view = prefab.GetComponent<ParticleView>();
        _view.Play();
    }
}