using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionMusic : MonoBehaviour {

    AudioSource speaker;

    [SerializeField]
    private AudioSource ambient;
    bool playMusic = false;

    public static actionMusic instance;
	void Awake () {
        speaker = GetComponent<AudioSource>();
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (playMusic)
        {
            speaker.volume = Mathf.MoveTowards(speaker.volume, 1, Time.deltaTime / 2);
            ambient.volume = Mathf.MoveTowards(ambient.volume, 0, Time.deltaTime / 2);
        }
           
        else
        {
            ambient.volume = Mathf.MoveTowards(ambient.volume, 1, Time.deltaTime);
            speaker.volume = Mathf.MoveTowards(speaker.volume, 0, Time.deltaTime);
        }
           
		
	}

    public void MusicState(bool playing)
    {
        if (playing == true && speaker.isPlaying == false)
        {
            speaker.Play();
            playMusic = true;
        }
        else if (playing == true)
        {
            playMusic = true;
        }
        else
        {
            playMusic = false;
        }
    }
}
