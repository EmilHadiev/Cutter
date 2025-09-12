using DG.Tweening;
using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshTarget))]
[RequireComponent(typeof(SphereCollider))]
public class BonusCut : MonoBehaviour, ICuttable
{
    [SerializeField] private MeshTarget _target;
    [SerializeField] private ParticleView _destroyParticle;

    private IGameplaySoundContainer _soundContainer;

    public event Action Cut;

    private void OnValidate()
    {
        _target ??= GetComponent<MeshTarget>();
        _target.GameobjectRoot ??= gameObject;
        _destroyParticle ??= GetComponentInChildren<ParticleView>();
    }

    private void Awake()
    {
        _target.enabled = false;
        transform.DORotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        _destroyParticle.Stop();
    }

    [Inject]
    private void Constructor(IGameplaySoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    public void TryActivateCut() => _target.enabled = true;

    public void DeactivateCut()
    {
        PlaySound();
        PlayView();
        Cut?.Invoke();
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