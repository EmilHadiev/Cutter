using UnityEngine;

public class RewardParticleViewChanger
{
    public void Change(string skinName, GameObject prefab)
    {
        if (skinName == AssetProvider.Particles.ElectricParticle.ToString())
        {
            ChangeParticleColor(prefab, Color.white);
            MultiplyScale(prefab, 0.1f);
        }

        if (skinName == AssetProvider.Particles.FireParticle.ToString())
            MultiplyScale(prefab, 3);

        if (skinName == AssetProvider.Particles.IceParticle.ToString())
        {
            MultiplyScale(prefab, 3);
            ChangeParticleColor(prefab, Color.white);
        }

        if (skinName == AssetProvider.Particles.LightParticle.ToString())
            MultiplyScale(prefab, 10);

        if (skinName == AssetProvider.Particles.SoulsParticle.ToString())
            MultiplyScale(prefab, 5);
    }

    private void MultiplyScale(GameObject prefab, float scale)
    {
        prefab.transform.localScale *= scale;
    }

    private void ChangeParticleColor(GameObject prefab, Color color)
    {
        if (prefab.TryGetComponent(out ParticleSystem system))
        {
            var main = system.main;
            main.startColor = color;
        }
    }
}