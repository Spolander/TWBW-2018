using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement playerInstance;
	Vector3 []targets;
	public int target = 1;
	public int last = 1;

	public float speed;

	private float step;



    //voidaan hallita pelaajan liikkumista 
    private bool canMove = true;
    public bool CanMove { set { canMove = value; } }

    [SerializeField]
    private Room startingRoom;
    void Awake()
    {
        playerInstance = this;
    }

	// Use this for initialization
	void Start () {
		targets = new Vector3[3];

        startingRoom.InitializeRoom(true);

	}
	
	// Update is called once per frame
	void Update () {

        //move the player if movement is enabled
        if (canMove)
        {
            step = speed * Time.deltaTime;

            if (target != 1)
            {

                if (last == 1)
                {

                    transform.position = Vector3.MoveTowards(transform.position, targets[target], step);

                    //if near door, activate room door reached, get boolean from door script
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targets[1], step);

                    if (Vector3.Distance(transform.position, targets[1]) < 0.05f)
                    {
                        last = 1;
                    }
                }
            }
        }

	}

	public void setTargets(Vector3 target0, Vector3 target1, Vector3 target2){
		targets [0] = target0;
		targets [1] = target1;
		targets [2] = target2;

        target = 1;
        last = 1;
	}

	public void setMovement(int index){
		
		last = target;
		target = index;
	}

}
