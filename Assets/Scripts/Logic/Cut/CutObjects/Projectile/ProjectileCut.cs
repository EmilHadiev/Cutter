using System;
using UnityEngine;
using Zenject;

public class ProjectileCut : MonoBehaviour, ICuttable
{
    public event Action Cut;

    private bool _isCut;

    private IGameplaySoundContainer _sound;

    private void OnEnable() => _isCut = false;

    [Inject]
    private void Constructor(IGameplaySoundContainer sound)
    {
        _sound = sound;
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
    }
}