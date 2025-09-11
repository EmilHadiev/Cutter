using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class ObstacleCut : MonoBehaviour, ICuttable, ICutSoundable
{
    [SerializeField] private Collider _collider;

    private IGameplaySoundContainer _soundContainer;
    private IFactory _factory;

    private MeshTarget _meshTarget;
    private GameObject _clone;

    [ContextMenu(nameof(OnValidate))]
    private void OnValidate()
    {
        _collider ??= GetComponent<Collider>();
    }

    private void Start()
    {
        CreateClone(gameObject);
    }

    [Inject] 
    private void Constructor(IFactory factory, IGameplaySoundContainer soundContainer)
    {
        _factory = factory;
        _soundContainer = soundContainer;
    }

    public void PlaySound()
    {
        _soundContainer.PlayWhenFree(SoundsName.AttackObstacleImpact);
    }

    public void TryActivateCut()
    {
        gameObject.SetActive(false);

        if (_collider != null)
            _collider.enabled = false;

        _meshTarget.enabled = true;
        _clone.gameObject.SetActive(true);
    }

    public virtual void DeactivateCut()
    {
        PlaySound();
        _meshTarget.enabled =false;
    }

    protected void CreateClone(GameObject prefab)
    {
        _clone = _factory.Create(prefab, transform.position, transform.rotation, null);
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