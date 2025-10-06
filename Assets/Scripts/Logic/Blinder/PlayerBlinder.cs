using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerBlinder : MonoBehaviour, IBlindable
{
    [SerializeField] private HideWall _hideWall;

    [Inject] private ICameraColorChanger _colorChanger;

    public void Blind()
    {
        _colorChanger.SetColor(Color.black);
        _hideWall.gameObject.SetActive(true);
    }
}
