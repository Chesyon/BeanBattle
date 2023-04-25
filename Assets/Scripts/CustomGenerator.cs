using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomGenerator : MonoBehaviour
{
    public Color[] beans;
    public List<Color> thisRoundBeans;
    public Color[] blankColorArray;

    public Quaternion[] stats;
    public List<Quaternion> thisRoundStats;
    public Quaternion[] blankStatArray;

    public string[] names;
    public List<string> thisRoundNames;
    public string[] blankNameArray;

    public InputField roundField;
    public int BeanCount;
    public Text BeanCountText;

    int beanSelected;

    public InputField beanSelectInput;

    public Slider colorSlider;
    public InputField colorInputText;

    public List<InputField> statFields;

    public InputField nameInput;

    public List<InputField> allTextFields;
    public Button startButton;

    public CosmeticLoader cosLoader;

    List<Color> blankColorList;
    List<Quaternion> blankQuaternionList;
    List<string> blankStringList;

    public GameObject beanSelect;
    public BeanList beanListScript;

    void Awake()
    {
        blankColorArray = new Color[0];
        blankStatArray = new Quaternion[0];
        blankNameArray = new string[0];
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
    }
    public void GenerateBeans()
    {
        blankColorList = new List<Color>();
        blankQuaternionList = new List<Quaternion>();
        blankStringList = new List<string>();
        thisRoundBeans = blankColorList;
        thisRoundStats = blankQuaternionList;
        thisRoundNames = blankStringList;
        //reset all used vars in PlayerPrefs / PlayerPrefsX
        for (int i = 0; i < BeanCount; i++)
        {
            Color colorToUse = Color.HSVToRGB(UnityEngine.Random.value, 1, 1);
            thisRoundBeans.Add(colorToUse);
            int hatNum = UnityEngine.Random.Range(0, 11);
            Quaternion statsToUse = new (i + 1, hatNum, 0, 0);
            thisRoundStats.Add(statsToUse);
            string nameToUse = "Bean " + statsToUse.x.ToString();
            thisRoundNames.Add(nameToUse);
        }
        if (roundField.text.Length != 0 && roundField.text != "0")
        {
            foreach (InputField textField in allTextFields)
            {
                textField.interactable = true;
            }
            beanSelectInput.text = "1";
            DisplayBeanColor();
            DispStats();
            startButton.interactable = true;
            colorSlider.interactable = true;
        }
    }
    public void DisplayBeanColor()
    {
        if (beanSelectInput.text.Length != 0)
        {
            if ((int)Int32.Parse(beanSelectInput.text) < 1) beanSelectInput.text = "1";
            if ((int)Int32.Parse(beanSelectInput.text) > BeanCount) beanSelectInput.text = BeanCount.ToString();
            beanSelected = (int)Int32.Parse(beanSelectInput.text);
            Color.RGBToHSV(thisRoundBeans[beanSelected - 1], out float h, out _, out _);
            cosLoader.gameObject.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(h, 1, 1);
            colorSlider.value = h;
            colorInputText.text = Mathf.Round(h * 100).ToString();
        }
    }

    public void SetBeanColorSlider()
    {
        thisRoundBeans[beanSelected-1] = Color.HSVToRGB(colorSlider.value, 1, 1);
        cosLoader.gameObject.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(colorSlider.value, 1, 1);
        colorInputText.text = Mathf.RoundToInt((colorSlider.value * 100)).ToString();
        ReloadCos();
    }

    public void SetBeanColorText()
    {
        float hueVal;
        if (colorInputText.text.Length != 0) { hueVal = float.Parse(colorInputText.text) / 100; }
        else { hueVal = 0; }
        thisRoundBeans[beanSelected - 1] = Color.HSVToRGB(hueVal, 1, 1);
        cosLoader.gameObject.GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(hueVal, 1, 1);
        colorSlider.value = hueVal;
        ReloadCos();
    }

    public void SetBeanName()
    {
        if (nameInput.text.Length != 0) { thisRoundNames[beanSelected - 1] = nameInput.text; }
        else nameInput.text = "Bean " + thisRoundStats[beanSelected - 1].x.ToString();
    }

    public void DispBeanName()
    {
        nameInput.text = thisRoundNames[beanSelected - 1];
    }

    public void DispStats()
    {
        for(int i = 0; i < 3; i++) statFields[i].text = thisRoundStats[beanSelected - 1][i + 1].ToString();
        ReloadCos();
    }

    public void StatChange(int stat)
    {
        foreach (InputField epicField in statFields) if (epicField.text.Length == 0) epicField.text = "0";
        if (stat == 1) 
        {
            thisRoundStats[beanSelected - 1] = new Quaternion(beanSelected, float.Parse(statFields[0].text), thisRoundStats[beanSelected - 1].z, thisRoundStats[beanSelected - 1].w);
            cosLoader.transform.eulerAngles = new Vector3(0, 0, 0);
            cosLoader.LoadCos((int)float.Parse(statFields[0].text));
        }
        if (stat == 2) { thisRoundStats[beanSelected - 1] = new Quaternion(beanSelected, thisRoundStats[beanSelected - 1].y, float.Parse(statFields[1].text), thisRoundStats[beanSelected - 1].w); }
        if (stat == 3) { thisRoundStats[beanSelected - 1] = new Quaternion(beanSelected, thisRoundStats[beanSelected - 1].y, thisRoundStats[beanSelected - 1].z, float.Parse(statFields[2].text)); }
    }
    void ReloadCos()
    {
        cosLoader.transform.eulerAngles = new Vector3(0, 0, 0);
        cosLoader.LoadCos((int)thisRoundStats[beanSelected - 1].y);
    }

    public void CloseBeanList()
    {
        beanSelect.SetActive(false);
    }

    public void OpenBeanList()
    {
        beanListScript.Refresh();
        beanSelect.SetActive(true);
    }

    public void SaveCurrentBean()
    {
        string currentBeans = PlayerPrefs.GetString("savedBeans");
        currentBeans += "|" + nameInput.text + "," + colorInputText.text + "," + statFields[0].text + "," + statFields[1].text + "," + statFields[2].text;
        PlayerPrefs.SetString("savedBeans", currentBeans);
    }
    // Update is called once per frame
    void Update()
    {
        try
        {
            BeanCount = (int)Mathf.Pow(2, Int32.Parse(roundField.text));
        }
        catch (FormatException)
        {
            BeanCount = 0;
        }
        if (BeanCount == 1) BeanCount = 0;
        BeanCountText.text = BeanCount.ToString() + " beans";
        if (BeanCount < 2)
        {
            foreach(InputField textField in allTextFields)
            {
                textField.interactable = false;
                textField.text = "";
            }
            startButton.interactable = false;
            colorSlider.interactable = false;
            colorSlider.value = 0;
        }
    }
    public void StartGame()
    {
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
    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
}