using System;
using UnityEngine;

[Serializable]
public struct MapColor
{
    [SerializeField] public int StartRange;
    [SerializeField] public int MaxRange;
    [SerializeField] public Color[] GroundColors;
    [SerializeField] public Color[] ObstacleColors;
    [SerializeField] public Color BackgroundColor;
}