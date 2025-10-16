using UnityEngine;


public abstract class MapItemColorChanger : MonoBehaviour
{
    [SerializeField] protected Color[] Colors;

    public abstract void ChangeColor();

    public void SetColors(Color[] colors) => Colors = colors;
}