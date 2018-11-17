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
            }
          
    }

   

}
