using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour {
    public bool paused;
	// Use this for initialization
	void Start () {
        paused = false;
	}
	public void pause()
    {
        if (paused)
        {
            paused = false;
        }
        else
        {
            paused = true;
        }
    }
	// Update is called once per frame
	void Update () {
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
	}
}
