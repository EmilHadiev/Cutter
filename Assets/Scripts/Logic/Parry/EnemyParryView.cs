using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyParryView
{
    private const float AttackSpeed = 0.25f;

    private readonly IGameplaySoundContainer _soundContainer;
    private readonly IEnemyAnimator _animator;

    private ParticleView _parryView;
    private ParticleView _stunView;
    private ParticleView _parryWindowView;

    public EnemyParryView(IGameplaySoundContainer container, IFactory factory, IEnemyAnimator animator, ParticlePosition parryPosition, ParticlePosition stunPosition)
    {
        _soundContainer = container;
        _animator = animator;

        CreateParryParticle(factory, parryPosition, AssetProvider.ParryParticle).Forget();
        CreateStunParticle(factory, stunPosition, AssetProvider.StunParticle).Forget();
        CreateParryWindowParticle(factory, parryPosition, AssetProvider.ParryWindowParticle).Forget();
    }

    public void ShowParryWindow()
    {
        _animator.SetAttackSpeed(AttackSpeed);
        _parryWindowView.Play();
    }

    public void CloseParryWindow()
    {
        _animator.ResetAttackSpeed();
        _parryWindowView.Stop();
    }

    public void ShowParryImpact()
    {
        _soundContainer.Play(SoundsName.ParryImpact);
        _animator.SetStunTrigger();
        _parryView.Play();
        _stunView.Play();
    }

    public void CloseParryImpact()
    {
        _animator.ResetStunTrigger();
        _animator.ResetAttackSpeed();
        _parryView.Stop();
        _stunView.Stop();
    }

    private async UniTaskVoid CreateStunParticle(IFactory factory, ParticlePosition position, string assetName)
    {
        Transform transform = position.transform;

        var prefab = await factory.Create(assetName, transform.position, transform.rotation, transform);
        _stunView = prefab.GetComponent<ParticleView>();
        _stunView.Stop();
    }

    private async UniTaskVoid CreateParryParticle(IFactory factory, ParticlePosition position, string assetName)
    {
        Transform transform = position.transform;

        var prefab = await factory.Create(assetName, transform.position, transform.rotation, transform);
        _parryView = prefab.GetComponent<ParticleView>();
        _parryView.Stop();
    }

    private async UniTaskVoid CreateParryWindowParticle(IFactory factory, ParticlePosition position, string assetName)
    {
        Transform transform = position.transform;

        var prefab = await factory.Create(assetName, transform.position, transform.rotation, transform);
        _parryWindowView = prefab.GetComponent<ParticleView>();
        _parryWindowView.Stop();
    }
}