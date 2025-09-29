using DG.Tweening.Core;
using UnityEngine;

public class ObstacleColorChanger : MapItemColorChanger
{
    [SerializeField] private ObstacleColorSetter[] _setter;

    private void OnValidate()
    {
        if (_setter.Length == 0)
            _setter = GetComponentsInChildren<ObstacleColorSetter>();
    }

    public override void SetColor()
    {
        for (int i = 0; i < _setter.Length; i++)
            _setter[i].SetColors(Colors);
    }
}