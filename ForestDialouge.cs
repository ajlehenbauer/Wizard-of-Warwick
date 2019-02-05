using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ForestDialouge : MonoBehaviour
{
    private Text message;
    private bool notHit;
    private AudioSource audioSource;
    public AudioClip textSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = textSound;
        //Create Text component of a canvas object through unity named "Message"
        message = GameObject.Find("Message").GetComponent<Text>();
        message.color = Color.white;
        message.text = "";
        notHit=true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && notHit)
        {
            StartCoroutine(displayText(other));

        }
        //Message to display if main dialouge is done
        else if(other.gameObject.tag == "Player" && !notHit)
        {
            other.gameObject.GetComponent<Player_Move>().playerSpeed = 0;
            message.text = "There is no going back";
            other.gameObject.GetComponent<Player_Move>().playerSpeed = 4;

        }
    }
    IEnumerator displayText(Collider2D other)
    {
        message.text = "An eerie feeling surrounds the forest";
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 0;
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        while (!Input.GetMouseButtonDown(0) && Input.touchCount!=1) {
        yield return null;
        }
        audioSource.Play();
        message.text = "you know you will never see your teacher again";
        yield return new WaitForSeconds(.5f);
        //
        while (!Input.GetMouseButtonDown(0) && Input.touchCount != 1)
        {
                yield return null;
        }
        audioSource.Play();

        message.text = "";
        yield return new WaitForSeconds(.5f);

        other.gameObject.GetComponent<Player_Move>().playerSpeed = 4;
        notHit=false;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
