using UnityEngine;

public interface IMovable
{
    Transform Transform { get; }
    FloatProperty MoveSpeed { get; }

    void SetMove(IMover mover);

    void StartMove();
    void StopMove();
}