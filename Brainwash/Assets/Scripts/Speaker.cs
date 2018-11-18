using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : Interactable{

    [SerializeField]
    private AudioSource question;

    public override void Interact()
    {
        if (question.isPlaying == false)
        {
            question.Play();
            StartCoroutine(playerWaitTime(question.clip.length));
        }
           
    }

    IEnumerator playerWaitTime(float time)
    {
        PlayerMovement.playerInstance.CanMove = false;
        yield return new WaitForSeconds(time);
        PlayerMovement.playerInstance.CanMove = true;
    }
}
