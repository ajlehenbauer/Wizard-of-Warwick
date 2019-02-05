using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour {

    private bool hit;
    private bool inRange;
    private void Start()
    {
        hit = false;
        inRange = false;
    }

    //TODO
    /// <summary>
    /// figure out how to make the dudes die in order
    /// </summary>
    private void Update()
    {
        if (inRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = true;
            }
        }
    }
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "crossHairs")
        {
            inRange = true;
        }

        if (collision.gameObject.tag == "knife")
        {
            Destroy(GetComponent<Rigidbody2D>());

            StartCoroutine(MoveFromToandDestroy(gameObject, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 15, 0),10));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "crossHairs")
        {
            inRange = false;
        }
    }
    IEnumerator MoveFromToandDestroy(GameObject enemy, Vector3 a, Vector3 b, float speed)
    {//https://gamedev.stackexchange.com/questions/100535/coroutine-to-move-to-position-passing-the-movement-speed
        Transform objectToMove = enemy.transform;
        float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = b;
        Destroy(enemy);
    }
}
