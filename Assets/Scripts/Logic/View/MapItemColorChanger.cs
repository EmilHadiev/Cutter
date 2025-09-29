using System.Collections;
using UnityEngine;


public abstract class MapItemColorChanger : MonoBehaviour
{
    [SerializeField] protected Color[] Colors;

    public abstract void SetColor();
}