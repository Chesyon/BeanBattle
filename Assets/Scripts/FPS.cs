using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    Text FPSCounter;
    float UpdateTimer;
    void Awake()
    {
        FPSCounter = gameObject.GetComponent<Text>();
        UpdateTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimer -= Time.deltaTime;
        if(UpdateTimer <= 0)
        {
            FPSCounter.text = "FPS: " + ((int)(1f / Time.unscaledDeltaTime)).ToString();
            UpdateTimer = 1;
        }
    }
}
