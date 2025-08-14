using System;

public interface ICharacterCutLogic
{
    event Action<int> CutTargets;
}