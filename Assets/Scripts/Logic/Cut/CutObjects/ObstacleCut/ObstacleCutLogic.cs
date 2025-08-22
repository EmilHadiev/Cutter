using DynamicMeshCutter;
using UnityEngine;

public class ObstacleCutLogic : BaseCutLogic
{
    public ObstacleCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter) : base(mouseBehaviour, cutTargetsCounter)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Obstacle);
    }
}
