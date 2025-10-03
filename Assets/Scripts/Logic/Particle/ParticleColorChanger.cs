using UnityEngine;

public class ParticleColorChanger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void ChangerColor(Color color)
    {
        if (_particle == null)
            _particle = GetComponent<ParticleSystem>();

        var main = _particle.main;
        main.startColor = color;
    }
}