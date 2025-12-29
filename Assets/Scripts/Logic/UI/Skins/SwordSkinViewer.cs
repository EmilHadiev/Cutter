using UnityEngine;
using Zenject;

public class SwordSkinViewer : MonoBehaviour
{
    private GameObject _currentSword;
    private GameObject _currentParticle;

    public void SetSword(GameObject sword, SkinData data)
    {
        if (_currentSword != null)
            Destroy(_currentSword);

        _currentSword = Instantiate(sword, transform);
        _currentSword.transform.localPosition = new Vector3(_currentSword.transform.localPosition.x, _currentSword.transform.localPosition.y, -5);

        float additionalSize = 1.5f;
        _currentSword.transform.localScale = _currentSword.transform.localScale * additionalSize;
    }

    public void SetParticle(GameObject particle, SkinData data)
    {
        if (_currentParticle != null)
            Destroy(_currentParticle);

        if (_currentSword == null)
            return;

        ParticlePosition particlePosition = _currentSword.GetComponentInChildren<ParticlePosition>();

        if (particlePosition == null)
            return;

        _currentParticle = Instantiate(particle, particlePosition.transform);
        SetParticlePosition();
    }

    private void SetParticlePosition()
    {
        _currentParticle.transform.localPosition = Vector3.zero;

        int bigParticle = 1;
        float particleSize = 0.5f;

        if (_currentParticle.transform.localScale.x > bigParticle)
            _currentParticle.transform.localScale = new Vector3(particleSize, particleSize, particleSize);
    }
}