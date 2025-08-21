using System;
using TMPro;
using UnityEngine;

public class Optimizator : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _frameCount;
    private float _prevTime;

    private int _steps = 0;
    private float _fpsSum = 0;

    public int CurrentFPS { get; private set; }
    public bool IsEnoughFPS => CurrentFPS >= 20;

    public void Update()
    {
        _frameCount++;
        float timePassed = Time.realtimeSinceStartup - _prevTime;

        if (timePassed >= 1f)
        {
            CurrentFPS = Convert.ToInt32(_frameCount / timePassed);
            _frameCount = 0;
            _prevTime = Time.realtimeSinceStartup;
            _steps++;
            _fpsSum += CurrentFPS;
            _text.text = CurrentFPS.ToString();
        }
    }

    public void OnDestroy()
    {
        Debug.Log("avg fps = " + GetAverageFPS());
    }

    public void Start()
    {
        QualitySettings.SetQualityLevel(0);
        Debug.Log("Current quality level + " + QualitySettings.GetQualityLevel());
    }

    private float GetAverageFPS() => _fpsSum / _steps;
}
