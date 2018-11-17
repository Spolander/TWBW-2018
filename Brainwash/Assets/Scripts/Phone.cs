using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone :Interactable {

    [SerializeField]
    private AudioSource speaker;

    public override void Interact()
    {
        if(speaker)
        if (speaker.isPlaying == false)

            {
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
   

}
