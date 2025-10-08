using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Armor : MonoBehaviour, IArmorable
{
    [SerializeField] private Helmet _armor;

    [Inject] private readonly IEnemySoundContainer _soundContainer;
    [Inject] private readonly IFactory _factory;

    private ParticleView _view;

    private bool IsValid => _armor != null && _armor.gameObject.activeInHierarchy == true && enabled == true;

    public bool IsCanCut
    {
        get
        {
            return IsValid == false;
        }
    }

    private void OnValidate()
    {
        _armor ??= GetComponentInChildren<Helmet>();
    }

    private void Awake()
    {
        _armor?.CutEnable(false);
        _armor?.EnableToggle(false);

        if (_armor != null)
            CreateParticle().Forget();
    }

    public void Activate()
    {
        _armor?.EnableToggle(true);
    }

    public void Deactivate()
    {
        _armor?.EnableToggle(false);
    }

    public void HandleFailCut()
    {
        _soundContainer.Play(SoundsName.ShieldImpact);
        _armor.CutEnable(true);
        _view.transform.parent = null;
        _view.Play();
    }

    private async UniTaskVoid CreateParticle()
    {
        Transform armor = _armor.transform;
        var prefab = await _factory.CreateAsync(AssetProvider.DefenseParticle, armor.position, armor.rotation, armor);
        _view = prefab.GetComponent<ParticleView>();
        _view.Stop();
    }
}