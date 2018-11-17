using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement playerInstance;
	Vector3 []targets;
	public int target = 1;
	public int last = 1;

    public int Last { get { return last; } }

	public float speed;

	private float step;



    //voidaan hallita pelaajan liikkumista 
    private bool canMove = true;
    public bool CanMove { set { canMove = value; }get { return canMove; } }

    [SerializeField]
    private Room startingRoom;


    //has the player reached a door or an interactable 
    private bool objectReached = false;
    public bool ObjectReached { set { objectReached = value; } }

    //target for player. if target is the phone for example, the player goes to the middle point and interacts with it
    private Interactable currentInteractTarget;
    public Interactable CurrentInteractTarget { set { currentInteractTarget = value; } }

    private float reachingDistance = 0.5f;

    private Room currentRoom;

    public Room CurrentRoom { set { currentRoom = value; } }

    private Door targetDoor;

    public Door TargetDoor { set { targetDoor = value; } }

    Animator anim;
    void Awake()
    {
        playerInstance = this;
    }

	// Use this for initialization
	void Start () {
		targets = new Vector3[3];

        startingRoom.InitializeRoom(true);
        currentRoom = startingRoom;
        Camera.main.transform.position = currentRoom.CameraTransform.position;
        Camera.main.transform.rotation = currentRoom.CameraTransform.rotation;
        transform.position = targets[target];
        anim = GetComponentInChildren<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        //move the player if movement is enabled
        if (canMove)
        {
            step = Mathf.MoveTowards(step, speed * Time.deltaTime,Time.deltaTime/3);

            if (target != 1)
            {

                if (last == 1)
                {

                    transform.position = Vector3.MoveTowards(transform.position, targets[target], step);

                    //if near door, activate room door reached, get boolean from door script
                    if (Vector3.Distance(transform.position, targets[target]) <= reachingDistance)
                    {
                        canMove = false;

                        //Do damage to player if needed
                        if (targetDoor.DoDamage)
                            Player.hitPoints -= targetDoor.Damage;

                        //jos tapetaan hahmo
                        if (targetDoor.KillTarget)
                        {
                            targetDoor.KillTarget.Play("death");
                        }
                      

                        //proceed to next room
                        currentRoom.DoorReached(targetDoor.RightAnswer, targetDoor.KillTarget ? true:false);

                        anim.SetBool("running", false);
                        step = 0;
                    }
                    else
                    {
                        anim.SetBool("running", true);
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, targets[1], step);

                    if (Vector3.Distance(transform.position, targets[1]) < 0.05f)
                    {
                        step = 0;
                        last = 1;
                    }
                }
            }
            else if(target == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, targets[target], step);

                if (Vector3.Distance(transform.position, targets[target]) <= reachingDistance && objectReached == false && currentInteractTarget != null)
                {
                    objectReached = true;
                    currentInteractTarget.Interact();
                    currentInteractTarget = null;
                    anim.SetBool("running", false);
                    step = 0;
                }
                else if (Vector3.Distance(transform.position, targets[target]) > reachingDistance)
                    anim.SetBool("running", true);
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

        if (target == index)
            return;

		last = target;
		target = index;
	}

}
