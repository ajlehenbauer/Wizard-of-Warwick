using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_System : MonoBehaviour {
    private GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float v = .008f;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y+3f, yMin, yMax);
        float x1 = gameObject.transform.position.x;
        float y1 = gameObject.transform.position.y;
        float dist = Mathf.Sqrt(Mathf.Pow(x - x1, 2) + Mathf.Pow(y - y1, 2));
        dist = dist / 5;
        if (x < x1-.055f)
        {
            x1-= v *dist;
        }
        else if (x > x1+.055f)
        {
            x1+=v * dist;
        }
        if (y < y1-.055f)
        {
            y1-=v * dist;
        }
        else if (y > y1+.05f)
        {
            y1+=v * dist;
        }
        gameObject.transform.position = new Vector3(x1, y1, gameObject.transform.position.z);
    }
}
