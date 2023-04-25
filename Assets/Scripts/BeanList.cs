using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeanList : MonoBehaviour
{
    public RectTransform thisRect;
    public string fileContent;
    public string[] fileLinesArr;
    public GameObject buttonToAdd;

    // Start is called before the first frame update
    public void Refresh()
    {
        foreach(Transform child in GetComponentsInChildren<Transform>())
        {
            if (child != transform) Destroy(child.gameObject); 
        }
        fileContent = PlayerPrefs.GetString("savedBeans");
        if (fileContent != "failed")
        {
            fileLinesArr = fileContent.Split("|");
            Vector2 newSize = new (thisRect.rect.size.x, fileLinesArr.Length * 200);
            Vector2 oldSize = thisRect.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            thisRect.offsetMin -= new Vector2(deltaSize.x * thisRect.pivot.x, deltaSize.y * thisRect.pivot.y);
            thisRect.offsetMax += new Vector2(deltaSize.x * (1f - thisRect.pivot.x), deltaSize.y * (1f - thisRect.pivot.y));
            for (int i = 0; i < fileLinesArr.Length; i++)
            {
                GameObject newButton = Instantiate(buttonToAdd);
                newButton.transform.SetParent(transform, false);
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
                RectTransform buttonRect = newButton.GetComponent<RectTransform>();
                BeanEntry buttonEntryScript = newButton.GetComponentInChildren<BeanEntry>();
                buttonEntryScript.value = fileLinesArr[i];
                buttonEntryScript.index = i;
                buttonRect.anchoredPosition = new Vector2(0, -200 * i - 100);
                buttonText.text = fileLinesArr[i];
            }
        }
    }

    // Update is called once per frame
    void Start()
    {
        Refresh();
    }
}
