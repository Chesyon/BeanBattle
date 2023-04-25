using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeanEntry : MonoBehaviour
{
    public int index;
    public string value;
    List<string> fileLinesList;
    string outputString;
    CustomGenerator cg;
    string buttonText;

    void Start()
    {
        cg = Camera.main.GetComponent<CustomGenerator>();
        TextMeshProUGUI thisText = GetComponentInChildren<TextMeshProUGUI>();
        foreach(string stat in value.Split(","))
        {
            buttonText += stat;
            buttonText += " / ";
        }
        buttonText = buttonText.Remove(buttonText.Length - 3);
        thisText.text = buttonText;
    }
    public void RemoveFromList()
    {
        string fileContent = PlayerPrefs.GetString("savedBeans");
        if (fileContent.Contains("|"))
        {
            string[] fileLinesArr = fileContent.Split("|");
            fileLinesList = new List<string>();
            foreach (string arrItem in fileLinesArr)
            {
                fileLinesList.Add(arrItem);
            }
            fileLinesList.RemoveAt(index);
            foreach (string listItem in fileLinesList)
            {
                outputString += listItem + "|";
            }
            outputString = outputString.Remove(outputString.Length - 1);
        }
        else outputString = "";
        PlayerPrefs.SetString("savedBeans", outputString);
        GetComponentInParent<BeanList>().Refresh();
    }

    public void LoadBean()
    {
        string[] valList = value.Split(",");
        cg.LoadBean(valList);
    }
}
