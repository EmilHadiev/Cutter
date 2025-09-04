public interface IDefensible : ICutCondition
{
    public bool IsCanDefending { get;}

    void Deactivate();
    void Activate();
}