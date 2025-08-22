using DynamicMeshCutter;
using UnityEngine;

public class CharacterCutLogic : BaseCutLogic
{
    public CharacterCutLogic(ICutMouseBehaviour mouseBehaviour) : base(mouseBehaviour)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Enemy);
    }
}