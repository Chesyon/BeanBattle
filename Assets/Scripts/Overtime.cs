using UnityEngine;
using UnityEngine.UI;

public class Overtime : MonoBehaviour
{
    public float timer;
    public float shrinkTime;
    public float shrinkSpeed;
    public Material shrinkMat;
    public MeshRenderer render;
    public Text timerText;
    public bool isActive = true;
    public GameObject vcam1;
    public GameObject vcam2;
    public LayerMask overtimeCamCull;
    void Update()
    {
        if(isActive)
        {
            timer += Time.deltaTime;
            if (timer >= shrinkTime)
            {
                vcam2.SetActive(true);
                vcam1.SetActive(false);
                render.material = shrinkMat;
                timerText.color = Color.red;
                transform.localScale -= new Vector3(Time.deltaTime * shrinkSpeed, 0, Time.deltaTime * shrinkSpeed);
                Camera.main.cullingMask = overtimeCamCull;
            }
        }
    }
}
