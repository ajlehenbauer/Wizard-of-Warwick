using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class tutorial1 : MonoBehaviour
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
        notHit = true;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && notHit)
        {
            StartCoroutine(displayText(other));

        }
        //Message to display if main dialouge is done
        else if (other.gameObject.tag == "Player" && !notHit)
        {
            message.text = "To teleport, first click the teleport button to  walk around";
        }
    }
    IEnumerator displayText(Collider2D other)
    {
        message.text = "My young pupil";
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 0;
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();
        message.text = "This is the last and most important step to achieve your destiny...";
        yield return new WaitForSeconds(.5f);
        //
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "and become a true Wizard of Warwick";
        yield return new WaitForSeconds(.5f);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "teleport up to discover the secrets of the previous Wizards";
        yield return new WaitForSeconds(.5f);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "";
        yield return new WaitForSeconds(.5f);


        //let player move again
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 4;
        notHit = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
