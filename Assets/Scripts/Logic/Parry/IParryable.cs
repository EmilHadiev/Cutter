using System;

public interface IParryable : ICutCondition
{
    void Activate();
    void Deactivate();
}