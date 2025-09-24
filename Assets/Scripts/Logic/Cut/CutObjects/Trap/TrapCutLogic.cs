using DynamicMeshCutter;
using UnityEngine;

public class TrapCutLogic : BaseCutLogic
{
    public TrapCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter, PlayerData playerData) : base(mouseBehaviour, cutTargetsCounter, playerData)
    {
    }

    protected override LayerMask GetLayerMask() => 
        LayerMask.GetMask(CustomMasks.Trap);

    protected override int GetMaxTargets(PlayerData playerData)
    {
        return playerData.NumberCutObstacles;
    }
}