using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public float SlowDownFactor = 0.05f;


    void Update()
    {
        Time.timeScale += (1f / SlowDownFactor) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp (Time.timeScale, 0f, 1f);
    }

    public void DoSlowMotion()
    {
        Time.timeScale = SlowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
