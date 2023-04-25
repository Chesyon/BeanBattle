using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgColorManager : MonoBehaviour
{
    public Image leftColor;
    public Image rightColor;
    // Start is called before the first frame update
    void Start()
    {
        leftColor.color = PlayerPrefsX.GetColor("lastWinnerColor");
        rightColor.color = PlayerPrefsX.GetColor("lastWinnerOppColor");
        if(rightColor.color.a == 0 || leftColor.color.a == 0)
        {
            PlayerPrefsX.SetColor("lastWinnerColor", Color.red);
            PlayerPrefsX.SetColor("lastWinnerOppColor", Color.blue);
            leftColor.color = Color.red;
            rightColor.color = Color.blue;
        }
    }
}
