using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour, IMovable
{
    public Transform Transform => transform;

    public FloatProperty MoveSpeed => throw new System.NotImplementedException();

    public void SetMove(IMover mover)
    {
        throw new System.NotImplementedException();
    }

    public void StartMove()
    {
        throw new System.NotImplementedException();
    }

    public void StopMove()
    {
        throw new System.NotImplementedException();
    }
}