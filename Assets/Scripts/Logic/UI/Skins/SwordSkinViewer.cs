using UnityEngine;

public class SwordSkinViewer : MonoBehaviour
{
    private GameObject _currentSword;
    private GameObject _currentParticle;

    public void SetSword(GameObject sword, SkinData data)
    {
        if (_currentSword != null)
            Destroy(_currentSword);

        _currentSword = Instantiate(sword, transform);
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

        if (_currentParticle.transform.localScale.x > bigParticle)
            _currentParticle.transform.localScale = new Vector3(1, 1, 1);
    }
}