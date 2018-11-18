using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone :Interactable {

    [SerializeField]
    private AudioSource speaker;

    
    private bool answered = false;

    [SerializeField]
    private AudioSource phoneRing;
    public override void Interact()
    {

        if (answered)
            return;

        if(speaker)
        if (speaker.isPlaying == false)

            {
                GetComponent<Animator>().speed = 0;
                GetComponent<Animator>().Play("phoneRing", 0, 0);
                answered = true;
                phoneRing.Stop();
                speaker.Play();
                StartCoroutine(phoneDelay(speaker.clip.length));
            }
          
    }
    IEnumerator phoneDelay(float seconds)
    {
        PlayerMovement.playerInstance.CanMove = false;

        yield return new WaitForSeconds(seconds);

        PlayerMovement.playerInstance.CanMove = true;
    }
    public void PhoneRingSound()
    {
        if(answered == false)
        phoneRing.Play();
    }

}
