using UnityEngine;

public class CosmeticLoader : MonoBehaviour
{
    public Object[] hats;
    public Color selfColor;
    GameObject hat;
    public bool CustomMenu;
    int layerToSet;
    // Start is called before the first frame update
    public void LoadCos(int cos)
    {
        //if bean has an existing hat, delete it
        if (hat != null) GameObject.Destroy(hat);

        //color and stat setup
        if (cos == -1)
        {
            if (gameObject.name == "Bean 1") { layerToSet = 7; selfColor = PlayerPrefsX.GetColorArray("beanColors")[0];  LoadCos((int)PlayerPrefsX.GetQuaternionArray("beanStats")[0].y); }
            if (gameObject.name == "Bean 2") { layerToSet = 8; selfColor = PlayerPrefsX.GetColorArray("beanColors")[1];  LoadCos((int)PlayerPrefsX.GetQuaternionArray("beanStats")[1].y); }
            if (gameObject.name == "Bean 3") { layerToSet = 9; selfColor = PlayerPrefsX.GetColorArray("beanColors")[2];  LoadCos((int)PlayerPrefsX.GetQuaternionArray("beanStats")[2].y); }
            if (gameObject.name == "Bean 4") { layerToSet = 10; selfColor = PlayerPrefsX.GetColorArray("beanColors")[3]; LoadCos((int)PlayerPrefsX.GetQuaternionArray("beanStats")[3].y); }
            if (gameObject.name == "Bean")
            {
                selfColor = PlayerPrefsX.GetColorArray("beanColors")[0];
                layerToSet = 0;
                LoadCos((int)PlayerPrefsX.GetQuaternionArray("beanStats")[0].y);
            }
        }
        //if (gameObject.name != "Bean") selfColor = gameObject.GetComponent<MeshRenderer>().material.color;

        //load cosmetic based off id
        if (cos == 1)
        {
            hat = Instantiate(hats[0], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(50, 50, 50); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(90, 0, 0);
            hat.transform.localPosition = new Vector3(0, 0.75f, 0);
            hat.GetComponent<MeshRenderer>().materials[1].color = selfColor;
            SetLayer(hat);
        }
        if (cos == 2)
        {
            hat = Instantiate(hats[1], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localPosition = new Vector3(0, 0.4f, 0.45f);
            SetLayer(hat);
        }
        if (cos == 3)
        {
            hat = Instantiate(hats[2], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localPosition = new Vector3(-0.25f, 0.4f, 0.5f);
            hat.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            SetLayer(hat);
        }
        if (cos == 4)
        {
            hat = Instantiate(hats[3], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localPosition = new Vector3(0, 1.35f, 0.05f);
            hat.GetComponent<MeshRenderer>().material.color = selfColor;
            SetLayer(hat);
        }
        if (cos == 5)
        {
            hat = Instantiate(hats[4], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(40, 40, 40); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(90, 90, 0);
            hat.transform.localPosition = new Vector3(-0.47f, 0.9f, -0.1f);
            hat.GetComponent<MeshRenderer>().material.color = selfColor;
            SetLayer(hat);
        }
        if (cos == 6)
        {
            hat = Instantiate(hats[5], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(40, 40, 40); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(-110, 0, 0);
            hat.transform.localPosition = new Vector3(0, 1.2f, -0.25f);
            SetLayer(hat);
        }
        if (cos == 7)
        {
            hat = Instantiate(hats[6], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(55, 55, 55); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(-90, 0, 0);
            hat.transform.localPosition = new Vector3(0, 1.65f, 0);
            SetLayer(hat);
        }
        if (cos == 8)
        {
            hat = Instantiate(hats[7], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(8, 8, 8); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(-55, -75, -110);
            hat.transform.localPosition = new Vector3(-0.15f, 0.9f, -0.1f);
            SetLayer(hat);
        }
        if (cos == 9)
        {
            hat = Instantiate(hats[8], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(7, 7, 7); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(-70, -90, 0);
            hat.transform.localPosition = new Vector3(-0.3f, 1, 0);
            SetLayer(hat);
        }
        if (cos == 10)
        {
            hat = Instantiate(hats[9], Vector3.zero, Quaternion.identity, transform) as GameObject;  // instatiate the object
            hat.transform.localScale = new Vector3(10, 10, 10); // change its local scale in x y z format
            hat.transform.localEulerAngles = new Vector3(-90, 0, 0);
            hat.transform.localPosition = new Vector3(0.1f, 1.1f, -0.25f);
            SetLayer(hat);
        }
    }
    void Start()
    {
        if(!CustomMenu) LoadCos(-1);
    }
    public void SetLayer(GameObject obj)
    {
        obj.layer = layerToSet;
    }
}
