using System;

public interface ICutTargetsCounter
{
    event Action<int> CutTargets;
    void AddCountTargets(int count);
}