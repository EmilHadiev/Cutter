using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class RewardParticleViewChanger
{
    public void Change(string skinName, GameObject prefab)
    {
        if (skinName == AssetProvider.Particles.ElectricParticle.ToString())
            SetElectricParticleOptions(prefab);
    }

    private void SetElectricParticleOptions(GameObject prefab)
    {
        float scale = 0.1f;
        prefab.transform.localScale = GetNewScale(scale);

        if (prefab.TryGetComponent(out ParticleSystem system))
        {
            var main = system.main;
            main.startColor = Color.white;
        }
    }

    private Vector3 GetNewScale(float scale) => new Vector3(scale, scale, scale);
}