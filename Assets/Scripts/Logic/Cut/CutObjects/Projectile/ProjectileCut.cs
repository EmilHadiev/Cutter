using System;
using UnityEngine;
using Zenject;

public class ProjectileCut : MonoBehaviour, ICuttable
{
    public event Action Cut;

    private bool _isCut;

    private IGameplaySoundContainer _sound;
    private ISlowMotion _slowMotion;

    private void OnEnable() => _isCut = false;

    [Inject]
    private void Constructor(IGameplaySoundContainer sound, ISlowMotion slowMotion)
    {
        _sound = sound;
        _slowMotion = slowMotion;
    }

    public void TryActivateCut()
    {
        
    }

    public void DeactivateCut()
    {
        if (_isCut)
            return;

        Cut?.Invoke();
        _isCut = true;
        _sound.Play(SoundsName.ParryImpact);
        _slowMotion.SlowDownTime();
    }
}