using DynamicMeshCutter;
using UnityEngine;

public class CharacterCutLogic : BaseCutLogic
{
    public CharacterCutLogic(ICutMouseBehaviour mouseBehaviour, ICutTargetsCounter cutTargetsCounter, PlayerData playerData) : base(mouseBehaviour, cutTargetsCounter, playerData)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Enemy);
    }

    protected override int GetMaxTargets(PlayerData playerData) => playerData.NumberEnemiesCut;
}