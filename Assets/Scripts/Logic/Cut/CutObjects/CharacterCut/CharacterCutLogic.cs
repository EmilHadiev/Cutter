using DynamicMeshCutter;
using UnityEngine;

public class CharacterCutLogic : BaseCutLogic
{
    public CharacterCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter) : base(mouseBehaviour, cutTargetsCounter)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Enemy);
    }
}