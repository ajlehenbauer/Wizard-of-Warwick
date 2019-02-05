using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    void Update()
    {
        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
    }
}