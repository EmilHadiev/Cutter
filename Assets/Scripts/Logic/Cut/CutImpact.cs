using System;
using Zenject;

public class CutImpact : IInitializable, IDisposable
{
    private readonly ISoundContainer _soundContainer;
    private readonly ICharacterCutLogic _cutLogic;

    public CutImpact(ISoundContainer soundContainer, ICharacterCutLogic cutLogic)
    {
        _soundContainer = soundContainer;
        _cutLogic = cutLogic;
    }

    public void Initialize()
    {
        _cutLogic.CutTargets += TryPlaySound;
    }

    public void Dispose()
    {
        _cutLogic.CutTargets -= TryPlaySound;
    }

    private void TryPlaySound(int countTargets)
    {
        if (countTargets <= 0)
            return;

        _soundContainer.Play(SoundsName.AttackFleshImpact);
    }
}