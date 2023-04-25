using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanEntry : MonoBehaviour
{
    public int index;
    public string value;
    List<string> fileLinesList;
    string outputString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveFromList()
    {
        string fileContent = PlayerPrefs.GetString("savedBeans");
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
        if(outputString != "") outputString = outputString.Remove(outputString.Length-1);
        PlayerPrefs.SetString("savedBeans", outputString);
        GetComponentInParent<BeanList>().Refresh();
    }

    public void LoadBean()
    {
        
    }
}
