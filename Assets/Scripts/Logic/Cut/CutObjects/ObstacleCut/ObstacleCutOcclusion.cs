using DynamicMeshCutter;
using NGS.AdvancedCullingSystem.Dynamic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshTarget))]
public class ObstacleCutOcclusion : MonoBehaviour, ICuttable
{
    [SerializeField] private MeshTarget _meshTarget;

    [Inject]
    private readonly IGameplaySoundContainer _soundContainer;

    private void OnValidate()
    {
        _meshTarget ??= GetComponent<MeshTarget>();
    }

    public void DeactivateCut()
    {
        _meshTarget.enabled = true;
    }

    public void TryActivateCut()
    {
        _soundContainer.PlayWhenFree(SoundsName.AttackObstacleImpact);
        _meshTarget.enabled = false;
    }
}