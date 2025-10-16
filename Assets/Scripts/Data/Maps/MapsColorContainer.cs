using System;
using UnityEngine;

[CreateAssetMenu(menuName = ScriptableObjectPath.MapColors, fileName = "MapsColor")]
public class MapsColorContainer : ScriptableObject
{
    [SerializeField] private MapColor[] _colors;

    public MapColor GetColor(int level)
    {
        int maxLevels = _colors[_colors.Length - 1].MaxRange;
        int currentLevel = level % maxLevels;
        Debug.Log(currentLevel);

        for (int i = 0; i < _colors.Length; i++)
        {
            if (_colors[i].StartRange <= currentLevel && currentLevel <= _colors[i].MaxRange)
                return _colors[i];
        }

        throw new ArgumentException(nameof(currentLevel) + " " + nameof(level));
    }
}