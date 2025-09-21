using CartoonFX;
using UnityEngine;

public class ParticleViewText : ParticleView
{
    [SerializeField] private CFXR_ParticleText _particleText;

    public void SetText(string text) => _particleText.UpdateText(text);
}