using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float countdown = 3;
    public Text VsText;

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        // manage countdown text
        if (countdown <= 1.5 && countdown > 1)
        {
            VsText.text = "3";
        }
        if (countdown <= 1 && countdown > 0.5)
        {
            VsText.text = "2";
        }
        if (countdown <= 0.5 && countdown > 0)
        {
            VsText.text = "1";
        }
        if (countdown <= 0 && countdown > -1)
        {
            VsText.text = "Start!";
            VsText.color = Color.white;
        }
        if (countdown < -1)
        {
            VsText.gameObject.SetActive(false);
        }
    }
}
