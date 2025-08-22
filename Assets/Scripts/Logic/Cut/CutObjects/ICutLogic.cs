using System;

public interface ICutLogic
{
    event Action<int> CutTargets;
}