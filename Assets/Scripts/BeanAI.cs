using UnityEngine;

public class BeanAI : MonoBehaviour
{
    GameObject Enemy;
    Rigidbody rb;
    Vector3 objectForward;
    float cooldown;
    public float maxCooldown;
    public float marginOfError;
    public bool isActive;
    AudioSource bonk;
    public Transform aligner;
    public Transform mid;
    public float distanceToMid;

    public Color[] beanColors;
    public Quaternion[] beanStats;
    public string[] beanNames;

    public Quaternion thisBeanStats;
    public string thisBeanName;

    public bool die;

    // Start is called before the first frame update
    void Start()
    {
        beanColors = PlayerPrefsX.GetColorArray("beanColors");
        beanStats = PlayerPrefsX.GetQuaternionArray("beanStats");
        beanNames = PlayerPrefsX.GetStringArray("beanNames");
        //find color from PlayerPrefsX
        if (gameObject.name == "Bean 1")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = beanColors[0];
            thisBeanStats = beanStats[0];
            thisBeanName = beanNames[0];
            //do other stuff with stats here
        }
        if (gameObject.name == "Bean 2")
        {
            gameObject.GetComponent<MeshRenderer>().material.color = beanColors[1];
            thisBeanStats = beanStats[1];
            thisBeanName = beanNames[1];
            //do other stuff with stats here
        }
        Color selfColor = gameObject.GetComponent<MeshRenderer>().material.color;
        TrailRenderer tr = GetComponentInChildren<TrailRenderer>();
        tr.startColor = selfColor;
        tr.endColor = new Color(selfColor.r, selfColor.g, selfColor.b, 0);
        tr.gameObject.SetActive(false);
        //setup variables
        cooldown = maxCooldown;
        bonk = GameObject.Find("bonkSound").GetComponent<AudioSource>();
        isActive = true;
        cooldown = 2;
        rb = gameObject.GetComponent<Rigidbody>();
        // find the enemy by going through all gameobjects and finding the one that is on the bean layer and isn't this gameobject
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach(GameObject go in gos)
        {
            if (go.layer == 3 && go != gameObject)
                Enemy = go;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!die)
        {
            distanceToMid = Vector3.Distance(transform.position, mid.position);
            aligner.transform.LookAt(mid);
            //do AI stuff if isActive is true
            if (isActive)
            {
                rb.isKinematic = false;
                //lock onto enemy and subtract cooldown
                transform.LookAt(Enemy.transform);
                objectForward = transform.forward;
                cooldown -= Time.deltaTime;
                //if cooldown is done, calculate error and jump towards enemy
                if (cooldown <= 0)
                {
                    cooldown = maxCooldown;
                    Vector3 error = new(Random.Range(-marginOfError, marginOfError), Random.Range(0, marginOfError * 5), Random.Range(-marginOfError, marginOfError));
                    rb.AddForce((objectForward + error) * 100);
                }
                //if far enough from the edge, move closer to center
                if (distanceToMid >= 3.5)
                {
                    rb.AddForce(100f * Time.deltaTime * aligner.forward);
                }
            }
            //disable physics if isActive is false
            else
            {
                rb.isKinematic = true;
            }
        }
    }
    //play bonk when colliding with enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 3) bonk.Play();
    }
}
