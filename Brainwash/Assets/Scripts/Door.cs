using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    private bool rightAnswer;

    public bool RightAnswer { get { return rightAnswer; } }

    [SerializeField]
    private bool doDamage = false;

    public bool DoDamage { get { return doDamage; } }

    [SerializeField]
    private int damage = 0;

    public int Damage { get { return damage; } }


    //sound that plays when you make a choice
    [SerializeField]
    private AudioSource soundAnswer;

    [SerializeField]
    private Animator killTarget;

    public Animator KillTarget { get { return killTarget; } }
}
