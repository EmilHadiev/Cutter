public interface ISoundContainer
{
    void Play(string soundName);
    void PlayWhenFree(string soundName);
    void Stop();
}