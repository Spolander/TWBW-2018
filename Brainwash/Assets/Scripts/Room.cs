using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField]
    private Room nextRoom;

    [SerializeField]
    private Transform[] wayPoints;

    public Transform[] WayPoints { get { return wayPoints; } }


    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private AudioSource phone;


    public void InitializeRoom(bool lastAnwserRight)
    {
        if(phone != null)
        phone.Play();

        PlayerMovement.playerInstance.setTargets(wayPoints[0].position, wayPoints[1].position, wayPoints[2].position);
    }
    //player calls this when near a chosen door
    public void DoorReached(bool rightAnswer)
    {

        //väärä vastaus
        if (rightAnswer == false)
        {
            Player.hitPoints -= 1;
            print("WRONG ANSWER BUDDY");

            if (Player.hitPoints <= 0)
            {


                gameOver(rightAnswer);
                return;
            }

        }

        //oikea vastaus
        else
        {
            if (Player.hitPoints <= 0)
            {
                //good ending
                gameOver(true);
            }
        }


      

        if (nextRoom)
        {
            ScreenFader.instance.Fade(0.5f);
            StartCoroutine(loadNextRoom(rightAnswer));

        }
        
    }


    IEnumerator loadNextRoom(bool rightAnswer)
    {

        yield return new WaitForSeconds(1f);
      

        //activate new room
        nextRoom.gameObject.SetActive(true);

        //initialize new room with data from last room
        nextRoom.InitializeRoom(rightAnswer);

        //position camera correctly
        Camera.main.transform.position = nextRoom.cameraTransform.position;
        Camera.main.transform.rotation = nextRoom.cameraTransform.rotation;

        //set new current Room for player
        PlayerMovement.playerInstance.CurrentRoom = nextRoom;

        //set player variables and position
        PlayerMovement.playerInstance.setTargets(nextRoom.wayPoints[0].position, nextRoom.wayPoints[1].position, nextRoom.wayPoints[2].position);
        PlayerMovement.playerInstance.transform.position = nextRoom.wayPoints[1].position;

        //PlayerMovement.playerInstance.CanMove = true;
        PlayerMovement.playerInstance.TargetDoor = null;



        //disable self
        gameObject.SetActive(false);
    }
    void gameOver(bool goodEnding)
    {
        print("Game over man");
    }
}
