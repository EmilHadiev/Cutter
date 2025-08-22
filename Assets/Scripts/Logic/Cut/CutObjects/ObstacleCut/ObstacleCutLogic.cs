using DynamicMeshCutter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObstacleCutLogic : BaseCutLogic
{
    public ObstacleCutLogic(ICutMouseBehaviour mouseBehaviour) : base(mouseBehaviour)
    {
    }

    protected override LayerMask GetLayerMask()
    {
        return LayerMask.GetMask(CustomMasks.Obstacle);
    }
}
