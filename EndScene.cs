using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class EndScene : MonoBehaviour
{
    private Text message;
    private bool notHit;
    
    void Start()
    {
      
        message = GameObject.Find("Message").GetComponent<Text>();
        message.color = Color.white;
        message.text = "";
        notHit=true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_System>().xMin = GameObject.FindGameObjectWithTag("Player").transform.position.x + 8;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Move>().playerSpeed = 0;
        message.text = "...";
        StartCoroutine(displayText(other));


    }

    IEnumerator displayText(Collider2D other)
    {

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        message.text = "To save Warwick...";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        message.text = "you must leave and find the source of this evil";
        yield return new WaitForSeconds(1);

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        message.text = "...";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
}
