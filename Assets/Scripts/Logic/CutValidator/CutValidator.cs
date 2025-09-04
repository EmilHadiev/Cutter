using System.Collections.Generic;

public class CutValidator
{
    private readonly List<ICutCondition> _conditions;

    public CutValidator(IEnumerable<ICutCondition> conditions)
    {
        _conditions = new List<ICutCondition>(conditions);
    }

    public bool IsCanCut()
    {
        foreach (var condition in _conditions)
            if (condition.IsCanCut == false)
                return false;

        return true;
    }

    public void HandleFailCut()
    {
        foreach (var condition in _conditions)
        {
            if (condition.IsCanCut == false)
            {
                condition.HandleFailCut();
                return;
            }
        }
    }
}