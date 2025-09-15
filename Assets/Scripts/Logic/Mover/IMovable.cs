using UnityEngine;

public interface IMovable
{
    Transform Transform { get; }
    void SetMove(IMover mover);

    void StartMove();
    void StopMove();
}