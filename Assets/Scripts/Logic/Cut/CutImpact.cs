using System;
using Zenject;

public class CutImpact : IInitializable, IDisposable
{
    private readonly ISoundContainer _soundContainer;
    private readonly ICutLogic _cutLogic;

    public CutImpact(ISoundContainer soundContainer, ICutLogic cutLogic)
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
        if (countTargets > 0)
            _soundContainer.Play(SoundsName.AttackFleshImpact);
        else
            _soundContainer.Play(SoundsName.MissImpact);
    }
}