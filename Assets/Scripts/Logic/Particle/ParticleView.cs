using UnityEngine;

public class ParticleView : MonoBehaviour
{
    /// <summary>
    /// first the Stop method will be called, 
    /// then the particle will play
    /// </summary>
    public void Play()
    {
        Stop();
        EnableToggle(true);
    }

    public void Stop() => EnableToggle(false);

    private void EnableToggle(bool isOn) => gameObject.SetActive(isOn);
}