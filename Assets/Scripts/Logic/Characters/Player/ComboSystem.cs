using UnityEngine;

public class ComboSystem : MonoBehaviour, IComboSystem
{
    [SerializeField] private ParticleViewText _particleText;

    private const int ComboMultiplier = 10;
    private const int ComboStarter = 1;

    public int GetComboReward => _combo * ComboMultiplier;

    private int _combo;

    private void Start()
    {
        _combo = 0;
        _particleText.Stop();
    }

    public void AddPoint()
    {
        _combo += 1;
        TryShowPoints();
    }

    private void TryShowPoints()
    {
        if (ComboStarter >= _combo)
            return;

        _particleText.SetText(_combo.ToString() + "x");
        _particleText.Play();
    }
}