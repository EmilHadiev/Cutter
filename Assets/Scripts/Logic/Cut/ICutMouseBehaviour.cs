using System;
using UnityEngine;

namespace DynamicMeshCutter
{
    public interface ICutMouseBehaviour
    {
        event Action CutEnded;
        event Action CutStarted;

        public void SetLineColor(Color color);
    }
}