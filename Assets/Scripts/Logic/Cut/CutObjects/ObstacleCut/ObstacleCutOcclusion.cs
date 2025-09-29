using DynamicMeshCutter;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshTarget))]
[RequireComponent(typeof(ObstacleColorSetter))]
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
        _soundContainer.PlayWhenFree(SoundsName.AttackObstacleImpact);
    }

    public void TryActivateCut()
    {
        _meshTarget.enabled = true;
    }
}