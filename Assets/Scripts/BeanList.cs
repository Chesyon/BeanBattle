using UnityEngine;
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
        // I want to implement the ability to read files at some point in the future but for now I'm just using a PlayerPrefs string.
        fileContent = PlayerPrefs.GetString("savedBeans");
        //Debug.Log(fileContent);
        if (fileContent != "failed" && fileContent != "")
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
                RectTransform buttonRect = newButton.GetComponent<RectTransform>();
                BeanEntry buttonEntryScript = newButton.GetComponentInChildren<BeanEntry>();
                buttonEntryScript.value = fileLinesArr[i];
                buttonEntryScript.index = i;
                buttonRect.anchoredPosition = new Vector2(0, -200 * i - 100);
            }
        }
    }

    // Update is called once per frame
    void Start()
    {
        Refresh();
    }
}
