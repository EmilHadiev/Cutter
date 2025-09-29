using NGS.AdvancedCullingSystem.Dynamic;
using UnityEngine;

public class EnemyHider : MonoBehaviour
{
    [SerializeField] private DC_SourceSettings _hider;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _sword;
    [SerializeField] private SkinnedMeshRenderer _mesh;

    private void OnValidate()
    {
        if (_hider == null)
        {
            gameObject.AddComponent<DC_SourceSettings>();
            _hider ??= GetComponent<DC_SourceSettings>();
            _hider.SourceType = SourceType.Custom;
        }  
    }

    public void Hide() => EnableToggle(false);

    public void Show() => EnableToggle(true);

    private void EnableToggle(bool isOn)
    {
        _mesh.enabled = isOn;
        _sword.SetActive(isOn);
        _shield.SetActive(isOn);
    }
}
