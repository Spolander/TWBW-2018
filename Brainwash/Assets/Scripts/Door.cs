using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField]
    private bool rightAnswer;

    public bool RightAnswer { get { return rightAnswer; } set { rightAnswer = value; } }

    [SerializeField]
    private bool doDamage = false;

    public bool DoDamage { get { return doDamage; } }

    [SerializeField]
    private int damage = 1;

    public int Damage { get { return damage; } }


    [SerializeField]
    private Animator killTarget;

    public Animator KillTarget { get { return killTarget; } }
}
