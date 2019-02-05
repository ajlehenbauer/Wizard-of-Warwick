using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Move : MonoBehaviour
{
    private GameObject camera;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public float inverseSpeed;
    public float offsetX;
    public float offsetY;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp(camera.transform.position.x- camera.transform.position.x / inverseSpeed, xMin, xMax);
        float y = Mathf.Clamp(camera.transform.position.y - camera.transform.position.y / inverseSpeed, yMin, yMax);
        
        gameObject.transform.position = new Vector3(x+offsetX, y+offsetY, gameObject.transform.position.z);
    }
}
