using UnityEngine;

public class DebugTimeScaleChanger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale -= 0.1f;
            Debug.Log("Changed timescale to " + Time.timeScale);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale += 0.1f;
            Debug.Log("Changed timescale to " + Time.timeScale);
        }
    }
}
