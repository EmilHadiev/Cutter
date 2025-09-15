using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SphereCollider))]
public class BonusCut : MonoBehaviour, ICuttable
{
    [SerializeField] private ParticleView _destroyParticle;

    private IGameplaySoundContainer _soundContainer;

    public event Action Cut;

    private void OnValidate()
    {
        _destroyParticle ??= GetComponentInChildren<ParticleView>();
    }

    private void Start()
    {
        transform.DOLocalMoveY(1, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _destroyParticle.Stop();
    }

    [Inject]
    private void Constructor(IGameplaySoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    public void TryActivateCut() { }

    public void DeactivateCut()
    {
        PlaySound();
        PlayView();
        Cut?.Invoke();
        gameObject.SetActive(false);
    }

    private void PlaySound()
    {
        _soundContainer.Stop();
        _soundContainer.Play(SoundsName.BonusItem);
    }

    private void PlayView()
    {
        _destroyParticle.transform.parent = null;
        _destroyParticle.Play();
    }
}