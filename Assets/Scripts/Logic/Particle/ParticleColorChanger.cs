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
        var main = _particle.main;
        main.startColor = color;
    }
}