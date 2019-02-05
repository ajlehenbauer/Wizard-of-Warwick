using System.Diagnostics;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC2 : MonoBehaviour
{
    private Text message;
    private bool notHit;
    private AudioSource audioSource;
    public AudioClip textSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = textSound;
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
        else if(other.gameObject.tag == "Player" && !notHit)
        {
            message.text = "Move on, and face your destiny";
        }
    }
    IEnumerator displayText(Collider2D other)
    {
        message.text = "...";
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 0;
        //will continue after mouse button is clicked
        //copy this block for every new line of dialouge
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "booOOooOOooo...";
        yield return new WaitForSeconds(1);
        //
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "...";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "since that didn't scare you off, you must be the next wizard";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();
        message.text = "There is a great darkness upon us, small wizard";
        
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();
        message.text = "There is no time to teach you the secrets";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "You must discover them on your journey";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "For now, these magical weapons will teach you to fight";
        yield return new WaitForSeconds(1);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();
        StartCoroutine(showKnives());
        message.text = "Go! Be wary of the danger ahead";
        yield return new WaitForSeconds(1);
        
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        audioSource.Play();

        message.text = "";
        yield return new WaitForSeconds(1);
        //let player move again
        other.gameObject.GetComponent<Player_Move>().playerSpeed = 4;
        notHit = false;
    }
    public IEnumerator showKnives()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] showKnives = GameObject.FindGameObjectsWithTag("showKnives");
        yield return new WaitForSeconds(1);
        for(int i = 0; i < showKnives.Length; i++)
        {
            showKnives[i].transform.position = new Vector3(player.transform.position.x+i+3,player.transform.position.y,player.transform.position.z);
        }
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.text = "";
        }
    }
}
