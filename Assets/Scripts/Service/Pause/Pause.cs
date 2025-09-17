using UnityEngine;

public class Pause : IPause
{
    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void Stop()
    {
        Time.timeScale = 0;
    }
}