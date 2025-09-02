using DynamicMeshCutter;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterCut : MonoBehaviour, ICuttable, ICutSoundable
{
    [SerializeField] private MeshTarget _target;

    private IHealth _health;
    private IDefensible _defenser;

    private bool _isActivated;

    [Inject]
    private ISoundContainer _soundContainer;

    private void OnValidate()
    {
        _target ??= GetComponentInChildren<MeshTarget>();
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _health = enemy.Health;
        _defenser = enemy.Defender;

        DeactivateCut();
    }

    public void TryActivateCut()
    {
         if (_defenser.TryDefend())
        {
            Debug.Log("ЗАЩИТА! активроана!");
            return;
        }

        _target.enabled = true;
        _isActivated = true;
    }

    public void DeactivateCut()
    {
        _target.enabled = false;
        TryToKill();
    }

    public void PlaySound()
    {
        _soundContainer.Stop();
        _soundContainer.Play(SoundsName.AttackFleshImpact);
    }

    private void TryToKill()
    {
        if (_isActivated)
        {
            PlaySound();
            _health.Kill();
            _isActivated = false;
        }
    }
}