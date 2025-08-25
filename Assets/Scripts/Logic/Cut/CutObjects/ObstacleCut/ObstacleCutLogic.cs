using DynamicMeshCutter;
using UnityEngine;

public class ObstacleCutLogic : BaseCutLogic
{
    public ObstacleCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter, PlayerData playerData) : base(mouseBehaviour, cutTargetsCounter, playerData)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Obstacle);
    }

    protected override int GetMaxTargets(PlayerData playerData) => playerData.NumberCutObstacles;
}
