using UnityEngine;

public class ProjectileView
{
    private ParticleView _view;

    public ProjectileView(ParticleView view, Color color, Transform transform)
    {
        _view = view;
        SettingProjectile(color);
    }

    public void Show() => _view.Play();

    public void Hide() => _view.Stop();

    private void SettingProjectile(Color color)
    {
        var colorChanger = _view.gameObject.AddComponent<ParticleColorChanger>();
        colorChanger.ChangerColor(color);
    }
}