using System;

namespace DynamicMeshCutter
{
    public interface ICutMouseBehaviour
    {
        event Action CutEnded;
        event Action CutStarted;
    }
}