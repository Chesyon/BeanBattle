using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeanGenerator : MonoBehaviour
{
    public Color[] beans;
    public List<Color> thisRoundBeans;
    Color[] blankColorArray;

    public Quaternion[] stats;
    public List<Quaternion> thisRoundStats;
    Quaternion[] blankStatArray;

    public string[] names;
    public List<string> thisRoundNames;
    string[] blankNameArray;

    public Text roundField;
    int BeanCount;

    public Toggle autoToggle;
    public Text BeanCountText;

    public Button startButton;

    public string fileContent;
    public string[] fileLinesArr;
    public List<List<string>> statsFromFile;
    // Start is called before the first frame update
    void Start()
    {
        //reset all used vars in PlayerPrefs / PlayerPrefsX
        blankColorArray = new Color[0];
        blankStatArray = new Quaternion[0];
        blankNameArray = new String[0];
        PlayerPrefs.SetInt("roundNumber", 1);
        PlayerPrefs.SetInt("maxRounds", 0);
        PlayerPrefs.SetInt("matchNumber", 1);
        PlayerPrefs.SetInt("maxMatches", 1);
        PlayerPrefsX.SetColorArray("beanColors", blankColorArray);
        PlayerPrefsX.SetColorArray("nextRoundBeans", blankColorArray);
        PlayerPrefsX.SetQuaternionArray("beanStats", blankStatArray);
        PlayerPrefsX.SetQuaternionArray("nextRoundStats", blankStatArray);
        PlayerPrefsX.SetStringArray("beanNames", blankNameArray);
        PlayerPrefsX.SetStringArray("nextRoundNames", blankNameArray);
        PlayerPrefs.SetString("savedBeans", "1,2,3|5,6,7");
        PlayerPrefsX.SetBool("AutoMode", false);

        PlayerPrefs.SetString("savedBeans", "bob,80,1,0,0|bluerasb,50,10,0,0|locton,70,9,0,0");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            BeanCount = (int)Mathf.Pow(2, int.Parse(roundField.text));
        }
        catch (FormatException)
        {
            BeanCount = 0;
        }
        if (BeanCount == 1) BeanCount = 0;
        BeanCountText.text = BeanCount.ToString() + " beans";
        if (BeanCount >= 2) startButton.interactable = true;
        else startButton.interactable = false;
    }
    public void StartGame()
    {
        //generate random color and stats for each bean and save it to the matching playerprefsx var
        for (int i = 0; i < BeanCount; i++)
        {
            Color colorToUse = Color.HSVToRGB(UnityEngine.Random.value, 1, 1);
            thisRoundBeans.Add(colorToUse);
            int hatNum = UnityEngine.Random.Range(0, 11);
            Quaternion statsToUse = new(i + 1, hatNum, 0, 0);
            thisRoundStats.Add(statsToUse);
            string nameToUse = "Bean " + statsToUse.x.ToString();
            thisRoundNames.Add(nameToUse);
        }
        beans = thisRoundBeans.ToArray();
        PlayerPrefsX.SetColorArray("beanColors", beans);
        stats = thisRoundStats.ToArray();
        PlayerPrefsX.SetQuaternionArray("beanStats", stats);
        names = thisRoundNames.ToArray();
        PlayerPrefsX.SetStringArray("beanNames", names);
        PlayerPrefs.SetInt("maxRounds", int.Parse(roundField.text));
        PlayerPrefs.SetInt("maxMatches", BeanCount / 2);
        SceneManager.LoadScene(1);
    }
    public void GoToCustom()
    {
        SceneManager.LoadScene(3);
    }

    public void ToggleAuto() 
    {
        PlayerPrefsX.SetBool("AutoMode", autoToggle.isOn);
    }
}
