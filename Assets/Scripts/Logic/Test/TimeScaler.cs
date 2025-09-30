using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField, Range(0, 1f)] private float _scale = 1f;

    private void Update()
    {
        Time.timeScale = _scale;
    }
}