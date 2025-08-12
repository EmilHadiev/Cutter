using System;
using UnityEngine;

public interface ICutPartContainer
{
    public event Action<GameObject> Added;
    void Add(GameObject cutPart);
}