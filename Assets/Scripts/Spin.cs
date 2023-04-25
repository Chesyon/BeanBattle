using UnityEngine;

public class Spin : MonoBehaviour
{
    void Update()
    {
        transform.eulerAngles += new Vector3(0, Time.deltaTime * 100, 0);
    }
}
