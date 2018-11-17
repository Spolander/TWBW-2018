using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoScreenDelay : MonoBehaviour {

    [SerializeField]
    private float waitTime = 6;

    [SerializeField]
    private Door[] doors;

    [SerializeField]
    private Animator anim;
    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
            doors[i].RightAnswer = false;

        StartCoroutine(ActivateGO());
    }

    IEnumerator ActivateGO()
    {
        anim.Play("Red");

        yield return new WaitForSeconds(waitTime);

        anim.Play("Green");

        for (int i = 0; i < doors.Length; i++)
            doors[i].RightAnswer = true;
    }
}
