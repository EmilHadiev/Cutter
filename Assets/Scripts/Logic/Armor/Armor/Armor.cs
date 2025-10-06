using UnityEngine;
using Zenject;

public class Armor : MonoBehaviour, IArmorable
{
    [SerializeField] private Helmet _armor;

    [Inject] private readonly IEnemySoundContainer _soundContainer;

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
        _armor.CutEnable(false);
        _armor.EnableToggle(false);
    }

    public void Activate() => _armor.EnableToggle(true);
    public void Deactivate() => _armor.EnableToggle(false);

    public void HandleFailCut()
    {
        _soundContainer.Play(SoundsName.ShieldImpact);
        _armor.CutEnable(true);
    }
}