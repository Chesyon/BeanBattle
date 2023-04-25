using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class MatchManager : MonoBehaviour
{
    public List<GameObject> beanList; // a list so we can access each bean
    bool firstWinFrame; //almost all of the win code is run on the first frame and shouldn't be run more then once
    public AudioSource fallSound; //the sound for when a bean falls

    //playerprefsx stuff
    List<Color> nextRoundBeansList;
    List<Color> beanColorsList;
    List<Quaternion> nextRoundStatsList;
    List<Quaternion> beanStatsList;
    List<string> nextRoundNamesList;
    List<string> beanNamesList;

    Color[] nextRoundBeans;
    Color[] beanColors;
    Quaternion[] nextRoundStats;
    Quaternion[] beanStats;
    string[] nextRoundNames;
    string[] beanNames;

    public List<Color> blankColorList;
    public List<Quaternion> blankStatList;
    public List<string> blankNameList;

    //get access to the overtime scirpt
    public GameObject floor;
    Overtime floorScript;

    public Text bottomText; //timer text

    // keep track of the current round and match
    int roundNumber;
    int matchNumber;

    // side bar text
    public Text RoundCounter;
    public Text MatchCounter;
    public Text Name1;
    public Text Name2;
    public Text VSText;

    public GameObject continueButton; //button that shows up after a bean falls

    //camera variables
    public GameObject camParent;
    CamID[] allCamsArr;
    public List<CamID> allCams;
    public string currentSide;
    public string currentAngle;
    //variables to help transition from bean cam to regular cam
    public string lastNonBeanAngle;
    public string lastNonBeanSide;
    bool usingBeanCam;
    //attempting cinemachine
    public CinemachineVirtualCamera cvc;

    public LayerMask defaultCull;
    public LayerMask bean1Cull;
    public LayerMask bean2Cull;

    public float winTime;
    float currentTime; //debug
    bool AutoMode;

    // Start is called before the first frame update
    void Start()
    {
        //setup variables
        firstWinFrame = true;
        floorScript = floor.GetComponent<Overtime>();
        nextRoundBeans = PlayerPrefsX.GetColorArray("nextRoundBeans");
        beanColors = PlayerPrefsX.GetColorArray("beanColors");
        nextRoundStats = PlayerPrefsX.GetQuaternionArray("nextRoundStats");
        beanStats = PlayerPrefsX.GetQuaternionArray("beanStats");
        nextRoundNames = PlayerPrefsX.GetStringArray("nextRoundNames");
        beanNames = PlayerPrefsX.GetStringArray("beanNames");
        roundNumber = PlayerPrefs.GetInt("roundNumber");
        matchNumber = PlayerPrefs.GetInt("matchNumber");
        AutoMode = PlayerPrefsX.GetBool("AutoMode");
        int maxRounds = PlayerPrefs.GetInt("maxRounds");
        int maxMatches = PlayerPrefs.GetInt("maxMatches");
        //create list of all beans in scene (hey that kinda rhymed)
        GameObject[] gos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in gos)
        {
            if (go.layer == 3)
            {
                beanList.Add(go);
            }
        }
        allCamsArr = camParent.GetComponentsInChildren<CamID>();
        foreach (CamID entry in allCamsArr) allCams.Add(entry);
        foreach (GameObject bean in beanList) allCams.Add(bean.GetComponentInChildren<CamID>());
        //update side ui
        Name1.color = beanColors[0];
        Name1.text = beanNames[0];
        Name2.color = beanColors[1];
        Name2.text = beanNames[1];
        RoundCounter.text = "Round " + roundNumber.ToString() + "/" + maxRounds.ToString();
        MatchCounter.text = "Match " + matchNumber.ToString()+ "/" + maxMatches.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        foreach(GameObject Bean in beanList)
        {
            if(Bean.transform.position.y <= 0)
            {
                //make bean die lol
                Bean.GetComponent<BeanAI>().die = true;
                beanList.Remove(Bean);
                break;
            }
        }
        //run below code when a bean wins
        if (beanList.Count == 1)
        {
            //get a reference to the winning bean, beanList should only contain the winning bean so we can just use a foreach on beanList.
            foreach (GameObject Bean in beanList)
            {
                if (firstWinFrame)
                {
                    fallSound.Play();
                    Bean.GetComponent<Collider>().isTrigger = true;
                    BeanAI winBeanAI = Bean.GetComponent<BeanAI>();
                    winBeanAI.isActive = false;
                    floorScript.isActive = false;
                    winTime = Time.timeSinceLevelLoad;

                    //update playerprefsx color stuff
                    nextRoundBeansList = new List<Color>(nextRoundBeans)
                    {
                        Bean.GetComponent<MeshRenderer>().material.color
                    };
                    PlayerPrefsX.SetColorArray("nextRoundBeans", nextRoundBeansList.ToArray());

                    beanColorsList = new List<Color>(beanColors);
                    beanColorsList.RemoveAt(1);
                    beanColorsList.RemoveAt(0);
                    PlayerPrefsX.SetColorArray("beanColors", beanColorsList.ToArray());

                    //update playerprefsx stats stuff
                    nextRoundStatsList = new List<Quaternion>(nextRoundStats)
                    {
                        winBeanAI.thisBeanStats
                    };
                    PlayerPrefsX.SetQuaternionArray("nextRoundStats", nextRoundStatsList.ToArray());

                    beanStatsList = new List<Quaternion>(beanStats);
                    beanStatsList.RemoveAt(1);
                    beanStatsList.RemoveAt(0);
                    PlayerPrefsX.SetQuaternionArray("beanStats", beanStatsList.ToArray());

                    //update playerprefsx names stuff
                    nextRoundNamesList = new List<string>(nextRoundNames)
                    {
                        winBeanAI.thisBeanName
                    };
                    PlayerPrefsX.SetStringArray("nextRoundNames", nextRoundNamesList.ToArray());

                    beanNamesList = new List<string>(beanNames);
                    beanNamesList.RemoveAt(1);
                    beanNamesList.RemoveAt(0);
                    PlayerPrefsX.SetStringArray("beanNames", beanNamesList.ToArray());

                    //UI stuff
                    bottomText.gameObject.SetActive(false);
                    VSText.text = "Winner!";
                    if (winBeanAI.gameObject.name == "Bean 1") Name2.gameObject.SetActive(false);
                    else Name1.gameObject.SetActive(false);

                    continueButton.SetActive(true);

                    firstWinFrame = false;
                }
                else Bean.transform.eulerAngles += new Vector3(0, Time.deltaTime * 100, 0);
            }
        }
        else bottomText.text = (Mathf.Round(floorScript.timer * 10) / 10).ToString();

        currentTime = Time.timeSinceLevelLoad;
        if (AutoMode && currentTime - 3 >= winTime && (winTime != 0)) Continue(); 
    }

    //run when the continue button is clicked
    public void Continue()
    {
        // run if this was the last match of the round
        if (beanColorsList.Count == 0)
        {
            //update PlayerPrefsX vars
            PlayerPrefsX.SetColorArray("beanColors", nextRoundBeansList.ToArray());
            PlayerPrefsX.SetColorArray("nextRoundBeans", blankColorList.ToArray());
            PlayerPrefsX.SetQuaternionArray("beanStats", nextRoundStatsList.ToArray());
            PlayerPrefsX.SetQuaternionArray("nextRoundStats", blankStatList.ToArray());
            PlayerPrefsX.SetStringArray("beanNames", nextRoundNamesList.ToArray());
            PlayerPrefsX.SetStringArray("nextRoundNames", blankNameList.ToArray());
            PlayerPrefs.SetInt("roundNumber", roundNumber + 1);
            PlayerPrefs.SetInt("matchNumber", 0);
            PlayerPrefs.SetInt("maxMatches", nextRoundBeansList.Count / 2);
        }
        if (nextRoundBeansList.Count == 1 && beanColorsList.Count == 0) SceneManager.LoadScene(2); //run if this was the final match
        else { PlayerPrefs.SetInt("matchNumber", PlayerPrefs.GetInt("matchNumber") + 1); SceneManager.LoadScene(1); } //run in any scenario that isn't the above 2
    }
    public void ToggleCam(string side, string angle)
    {
        currentSide = side;
        currentAngle = angle;
        foreach(CamID cam in allCams)
        {
            if (cam.side == side && cam.angle == angle)
            {
                cam.isActiveCam = true;
                cvc.Follow = cam.transform;
            }
        }
    }
    public void SetAngle(string angleToSet)
    {
        if(usingBeanCam)
        { 
            usingBeanCam = false; currentSide = lastNonBeanSide;
            SetDamping(true);
            Camera.main.cullingMask = defaultCull;
        }
        currentAngle = angleToSet;
        ToggleCam(currentSide, currentAngle);
    }
    public void SetSide(string sideToSet)
    {
        if(usingBeanCam)
        { 
            usingBeanCam = false; currentAngle = lastNonBeanAngle;
            SetDamping(true);
            Camera.main.cullingMask = defaultCull;
        }
        currentSide = sideToSet;
        ToggleCam(currentSide, currentAngle);
    }
    public void EnableBeanCam(string bean)
    {
        if(floorScript.timer < floorScript.shrinkTime)
        {
            if (!usingBeanCam)
            {
                lastNonBeanAngle = currentAngle;
                lastNonBeanSide = currentSide;
                SetDamping(false);
            }
            usingBeanCam = true;
            if (bean == "bean1") Camera.main.cullingMask = bean1Cull;
            else Camera.main.cullingMask = bean2Cull;
            ToggleCam(bean + "side", bean + "angle");
        }
    }
    void SetDamping(bool enable)
    {
        int dampLevels = 0;
        CinemachineTransposer cvct = cvc.GetCinemachineComponent<CinemachineTransposer>();
        if (enable) dampLevels = 2;
        cvct.m_XDamping = dampLevels;
        cvct.m_YDamping = dampLevels;
        cvct.m_ZDamping = dampLevels;
        cvc.GetCinemachineComponent<CinemachineSameAsFollowTarget>().m_Damping = dampLevels;
    }
}
