using System;

public class CutTargetsCounter : ICutTargetsCounter
{
    public event Action<int> CutTargets;

    public void AddCountTargets(int count)
    {
        CutTargets?.Invoke(count);
    }
}