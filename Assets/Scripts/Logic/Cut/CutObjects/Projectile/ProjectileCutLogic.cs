using DynamicMeshCutter;
using UnityEngine;

public class ProjectileCutLogic : BaseCutLogic
{
    public ProjectileCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter, PlayerData playerData) : base(mouseBehaviour, cutTargetsCounter, playerData)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Projectile);
    }

    protected override int GetMaxTargets(PlayerData playerData)
    {
        return playerData.NumberCutObstacles;
    }
}