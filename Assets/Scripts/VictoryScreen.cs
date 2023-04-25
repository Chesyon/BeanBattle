using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public Color winningBeanColor;
    public Quaternion winningBeanStats;
    public string winningBeanName;
    public ParticleSystem.MainModule particles;
    public Camera cam;
    public Text winText;
    Transform winningBean;
    // Start is called before the first frame update
    void Start()
    {
        winningBean = GameObject.Find("Bean").transform;
        //setup variables
        winningBeanColor = PlayerPrefsX.GetColorArray("beanColors")[0];
        winningBeanStats = PlayerPrefsX.GetQuaternionArray("beanStats")[0];
        winningBeanName = PlayerPrefsX.GetStringArray("beanNames")[0];
        particles = GameObject.Find("Particle System").GetComponent<ParticleSystem>().main;
        //make background the winning bean color but opposite hue
        Color.RGBToHSV(winningBeanColor, out float h, out _, out _);
        if (h - 0.5 < 0) h = 1 - 0.5f + h;
        else h -= 0.5f;
        cam.backgroundColor = Color.HSVToRGB(h, 1, 1);
        PlayerPrefsX.SetColor("lastWinnerColor",winningBeanColor);
        PlayerPrefsX.SetColor("lastWinnerOppColor", Color.HSVToRGB(h, 1, 1));
        //set other stuff to winning bean color
        particles.startColor = winningBeanColor;
        GameObject.Find("Bean").GetComponent<MeshRenderer>().material.color = winningBeanColor;

        winText.text = winningBeanName + " wins!";
    }
    void Update()
    {
        winningBean.eulerAngles += new Vector3(0, Time.deltaTime * 100, 0);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
