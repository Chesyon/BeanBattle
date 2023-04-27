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
    void Update()
    {
        if(isActive)
        {
            timer += Time.deltaTime;
            if (timer >= shrinkTime)
            {
                render.material = shrinkMat;
                timerText.color = Color.red;
                transform.localScale -= new Vector3(Time.deltaTime * shrinkSpeed, 0, Time.deltaTime * shrinkSpeed);
            }
        }
    }
}
