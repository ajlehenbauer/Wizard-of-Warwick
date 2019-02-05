using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    private bool notHit;
    private Text message;
    private GameObject camera;
    public GameObject e1;
    public GameObject e2;
    public GameObject e3;
    public GameObject e4;
    public GameObject e5;
    public GameObject k1;
    public GameObject k2;
    public GameObject k3;
    public GameObject k4;
    public GameObject k5;
    private GameObject cH;
    private GameObject[] lights;
    private float h;
    private float w;
    private LineRenderer lr;
    private Vector3 leftCorner;
    private Vector3 rightCorner;
    private bool fire;
    private Vector3 playerSpawn;
    private Vector3 otherSpawn;
    Vector3[] clicks;
    int c;
    GameObject[] knives;

    // Use this for initialization

    void Start () {
        playerSpawn = GameObject.FindGameObjectWithTag("Player").transform.position;
        otherSpawn = GameObject.FindGameObjectWithTag("knife").transform.position;
        h = 5;
        w = 19;
        lights = GameObject.FindGameObjectsWithTag("attack spot");
        knives = new GameObject[]{k1,k2,k3,k4,k5};
        c = 0;
        clicks = new Vector3[5];
        cH = GameObject.FindGameObjectWithTag("crossHairs");
        /*
        lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = 7;
        lr.startWidth=.1f;
        lr.endWidth = .1f;
        lr.material = new Material(Shader.Find("Standard"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        */
        notHit = true;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        fire = false;
        
        message = GameObject.Find("Message").GetComponent<Text>();
        message.color = Color.white;
        message.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if (fire && c<5 && cH.transform.position != rightCorner)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                clicks[c] = cH.transform.position;
                c++;
            
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "Player" && notHit)
        {
          //  lights[0].GetComponent<Light>().intensity = 10;
          //  lights[1].GetComponent<Light>().intensity = 10;
            notHit = false;
            message.text = "you must be careful";

            StartCoroutine(attack(other));
        }
        else if (other.gameObject.tag == "Player")
        {
            message.text = "you must be careful";
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        message.text = "";
    }

    IEnumerator attack(Collider2D other)
    {
        message.text = "!";
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 0;

        yield return new WaitForSeconds(1.5f);

        e1.transform.position = new Vector3(camera.transform.position.x - w * 2 / 5, camera.transform.position.y - h*2 , 0);
        e2.transform.position = new Vector3(camera.transform.position.x - w * 1 / 5, camera.transform.position.y - h*2, 0);
        e3.transform.position = new Vector3(camera.transform.position.x , camera.transform.position.y - h*2, 0);
        e4.transform.position = new Vector3(camera.transform.position.x + w * 1 / 5, camera.transform.position.y - h*2, 0);
        e5.transform.position = new Vector3(camera.transform.position.x + w * 2 / 5, camera.transform.position.y - h*2, 0);
        
        
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(e1.transform, e1.transform.position, new Vector3(camera.transform.position.x - w * 2 / 5, camera.transform.position.y + Random.Range(-h / 2, h / 2) + 1, 0), 7));
        StartCoroutine(MoveFromTo(e2.transform, e2.transform.position, new Vector3(camera.transform.position.x - w * 1 / 5, camera.transform.position.y + Random.Range(-h / 2, h / 2) + 1, 0), 7));
        StartCoroutine(MoveFromTo(e3.transform, e3.transform.position, new Vector3(camera.transform.position.x, camera.transform.position.y + 2, 0), 7));
        StartCoroutine(MoveFromTo(e4.transform, e4.transform.position, new Vector3(camera.transform.position.x + w * 1 / 5, camera.transform.position.y + Random.Range(-h / 2, h / 2) + 1, 0), 7));
        StartCoroutine(MoveFromTo(e5.transform, e5.transform.position, new Vector3(camera.transform.position.x + w * 2 / 5, camera.transform.position.y + Random.Range(-h / 2, h / 2) + 1, 0), 7));
        message.text = "You're under attack";
        yield return new WaitForSeconds(3);
        other.GetComponent<Animator>().SetBool("beingAttacked", true);
        message.text = "";

        leftCorner = Camera.current.ScreenToWorldPoint(new Vector3(0, 0, 0));
        leftCorner.z = 0;
        rightCorner = Camera.current.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        rightCorner.z = 0;
        /*
        lr.SetPosition(0,leftCorner);
        lr.SetPosition(1, e1.transform.position);
        lr.SetPosition(2, e2.transform.position);
        lr.SetPosition(3, e3.transform.position);
        lr.SetPosition(4, e4.transform.position);
        lr.SetPosition(5, e5.transform.position);
        lr.SetPosition(6, rightCorner);
        */
        cH.transform.position = new Vector3(leftCorner.x, leftCorner.y, 0);
        message.text = "click when cross hairs pass over ememies";
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        fire = true;
        message.text = "";
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, e1.transform.position, 5));
        while (cH.transform.position != e1.transform.position)
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, e2.transform.position, 5));
        while (cH.transform.position != e2.transform.position)
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, e3.transform.position, 5));
        while (cH.transform.position != e3.transform.position)
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, e4.transform.position, 5));
        while (cH.transform.position != e4.transform.position)
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, e5.transform.position, 5));
        while (cH.transform.position != e5.transform.position)
        {
            yield return null;
        }
        StartCoroutine(MoveFromTo(cH.transform, cH.transform.position, new Vector3(rightCorner.x, rightCorner.y, 0), 5));
        
        

        while (cH.transform.position != rightCorner)
        {
            yield return null;
        }
        Destroy(lr);
        cH.transform.position = otherSpawn;
        for (int i = 0; i < 5; i++)
        {
            
            if (clicks[i] != new Vector3(0,0,0))
            {
                knives[i].transform.position = other.transform.position;
                float angle = AngleBetweenVector2(knives[i].transform.position, clicks[i]);
                //message.text += angle+", ";
                knives[i].transform.Rotate(0,0,angle);
                float throwFarX = clicks[i].x + (-knives[i].transform.position.x + clicks[i].x) * 10;
                float throwFarY = clicks[i].y + (-knives[i].transform.position.y + clicks[i].y) * 10;

                StartCoroutine(MoveFromTo(knives[i].transform,knives[i].transform.position , new Vector3(throwFarX, throwFarY, 0), 12));
            }
            
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(returnAttack());
       
        other.GetComponent<Animator>().SetBool("beingAttacked", false);
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 5;



    }
    IEnumerator returnAttack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                knives[i].transform.position = enemies[i].transform.position;
                float angle = AngleBetweenVector2(knives[i].transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
                knives[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                knives[i].transform.Rotate(0, 0, -angle+180);
                Destroy(knives[i].GetComponent<Collider2D>());
                
                StartCoroutine(MoveFromTo(knives[i].transform, enemies[i].transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, 12));
            }
            yield return new WaitForSeconds(1.5f);
            respawn();
        }
    }

    float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 vec3 = new Vector2(vec1.x,vec2.y);
        float sign = (vec2.x > vec1.x) ? -1.0f : 1.0f;
        float dist1 = Mathf.Sqrt(Mathf.Pow(vec1.x - vec2.x, 2) + Mathf.Pow(vec2.y - vec1.y, 2));
        float dist2 = Mathf.Sqrt(Mathf.Pow(vec1.x - vec3.x, 2) + Mathf.Pow(vec3.y - vec1.y, 2));
        return Mathf.Acos(dist2/dist1)*sign*180/Mathf.PI;
    }
    void respawn()
    {
      
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] knives = GameObject.FindGameObjectsWithTag("knife");
        GameObject.FindGameObjectWithTag("Player").transform.position = playerSpawn;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Move>().moveTo = GameObject.FindGameObjectWithTag("Player").transform.position;
        
        for(int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = otherSpawn;
            
        }
        for(int i = 0; i < knives.Length; i++)
        {
            knives[i].transform.position = otherSpawn;
        }
    }
    IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed)
    {//https://gamedev.stackexchange.com/questions/100535/coroutine-to-move-to-position-passing-the-movement-speed
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        
    }
}
