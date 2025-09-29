using UnityEngine;

public class GroundColorChanger : MapItemColorChanger
{
    [SerializeField] private Ground[] _grounds;

    private void OnValidate()
    {
        if (_grounds.Length == 0)
            _grounds = GetComponentsInChildren<Ground>();
    }

    public override void SetColor()
    {
        for (int i = 0; i < _grounds.Length; i++)
            _grounds[i].SetColors(Colors);
    }
}