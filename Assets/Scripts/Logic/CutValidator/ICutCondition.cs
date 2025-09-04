public interface ICutCondition
{
    /// <summary>
    /// updated in the tickable zenject cycle
    /// </summary>

    bool IsCanCut { get; }
    void HandleFailCut();
}
