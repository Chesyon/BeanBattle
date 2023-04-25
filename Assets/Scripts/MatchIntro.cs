using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MatchIntro : MonoBehaviour
{
    public float startingCountdown;
    float countdown;
    public List<BeanAI> beanList;
    bool firstCountdownEndFrame;
    public List<TrailRenderer> trails;
    public List<GameObject> objectsToEnable;
    public List<GameObject> objectsToDisable;
    public List<Text> textObjects;
    public MeshRenderer bg;
    public MatchManager mm;
    public CinemachineVirtualCamera cvc;

    // Start is called before the first frame update
    void Start()
    {
        firstCountdownEndFrame = true;
        foreach(BeanAI bean in beanList)
        {
            bean.isActive = false;
            bean.gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        beanList[0].gameObject.transform.position = new Vector3(0, 7.25f, 2);
        beanList[1].gameObject.transform.position = new Vector3(0, 5.25f, -2);
        countdown = startingCountdown;
        for(int i = 0; i < 2; i++)
        {
            textObjects[i].text = PlayerPrefsX.GetStringArray("beanNames")[i];
            Color thisBeanColor = beanList[i].GetComponent<MeshRenderer>().material.color;
            Color.RGBToHSV(thisBeanColor, out float h, out _, out _);
            if (h - 0.5 < 0) h = 1 - 0.5f + h;
            else h -= 0.5f;
            bg.materials[i].color = Color.HSVToRGB(h, 1, 1);
        }
        cvc.Follow = transform;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        //other countdown stuff
        if (countdown <= 0 && firstCountdownEndFrame)
        {
            foreach (BeanAI bean in beanList)
            {
                bean.isActive = true;
            }
            beanList[0].gameObject.transform.position = new Vector3(0, 1, 4);
            beanList[1].gameObject.transform.position = new Vector3(0, 1, -4);
            firstCountdownEndFrame = false;
            foreach(GameObject enableObject in objectsToEnable)
            {
                enableObject.SetActive(true);
            }
            foreach (GameObject disableObject in objectsToDisable)
            {
                disableObject.SetActive(false);
            }
            mm.ToggleCam("bottom", "angled");
            gameObject.SetActive(false);
        }
    }
}
