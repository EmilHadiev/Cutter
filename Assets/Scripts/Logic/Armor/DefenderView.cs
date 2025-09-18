using Cysharp.Threading.Tasks;
using UnityEngine;

public class DefenderView
{
    private readonly Shield _shield;

    private ParticleView _particle;

    public DefenderView(IFactory factory, Shield shield)
    {
        _shield = shield;
        CreateParticle(factory, _shield.transform).Forget();
    }

    public void Play()
    {
        _shield.PlaySound();
        _particle.Play();
    }

    private async UniTaskVoid CreateParticle(IFactory factory, Transform parent)
    {
        var prefab = await factory.CreateAsync(AssetProvider.DefenseParticle);
        _particle = prefab.GetComponent<ParticleView>();

        _particle.transform.position = parent.transform.position;
        _particle.transform.rotation = parent.transform.rotation;
        _particle.transform.parent = parent;

        _particle.Stop();
    }
}