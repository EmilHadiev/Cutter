using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class ObstacleCut : MonoBehaviour, ICuttable
{
    [SerializeField] private Collider _collider;

    [Inject]
    private readonly IInstantiator _instantiator;

    private MeshTarget _meshTarget;
    private GameObject _clone;

    private void OnValidate()
    {
        _collider ??= GetComponent<Collider>();
    }

    private void Start()
    {
        CreateClone(gameObject);
    }

    public void ActivateCut()
    {
        gameObject.SetActive(false);

        if (_collider != null)
            _collider.enabled = false;

        _meshTarget.enabled = true;
        _clone.gameObject.SetActive(true);
    }

    public void DeactivateCut()
    {
        _meshTarget.enabled = false;
    }

    protected void CreateClone(GameObject prefab)
    {
        _clone = _instantiator.InstantiatePrefab(prefab, transform.position, transform.rotation, null);
        _clone.gameObject.SetActive(false);
        SetScale(prefab.transform.localScale);
        AddMeshTarget();
    }

    private void AddMeshTarget()
    {
        _clone.AddComponent<MeshTarget>();
        _meshTarget = _clone.GetComponent<MeshTarget>();
        _meshTarget.GameobjectRoot = _clone;
        _meshTarget.enabled = false;
    }

    protected void SetScale(Vector3 scale) => _clone.transform.localScale = scale;
}