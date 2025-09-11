using UnityEngine;

public class AmbientSoundContainer : SoundContainer, IAmbientSoundContainer
{
    private string[] _ambientSounds;

    private void Start()
    {
        _ambientSounds = new string[] {SoundsName.Ambient1, SoundsName.Ambient2 };
        PlayRandomAmbient();
    }

    public void PlayRandomAmbient()
    {
        int index = Random.Range(0, _ambientSounds.Length);
        string sound = _ambientSounds[index];

        SetPitch(GetRandomPitch());
        PlayCycle(sound);
    }

    private float GetRandomPitch() => Random.Range(0.6f, 0.75f);
}