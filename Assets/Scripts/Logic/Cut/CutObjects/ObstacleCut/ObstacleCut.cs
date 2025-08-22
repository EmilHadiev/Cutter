using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class ObstacleCut : MonoBehaviour, ICuttable
{
    [Inject]
    private readonly IInstantiator _instantiator;

    private MeshTarget _meshTarget;
    private GameObject _clone;

    private void Start()
    {
        CreateClone(gameObject);
    }

    public void ActivateCut()
    {
        gameObject.SetActive(false);
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
        _clone.transform.localScale = prefab.transform.localScale;
        AddMeshTarget();
    }

    private void AddMeshTarget()
    {
        _clone.AddComponent<MeshTarget>();
        _meshTarget = _clone.GetComponent<MeshTarget>();
        _meshTarget.GameobjectRoot = _clone;
        _meshTarget.enabled = false;
    }
}