using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    [SerializeField]
    private Room nextRoom;

    [SerializeField]
    private Transform[] wayPoints;


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
        if (rightAnswer == false)
        {
            Player.hitPoints -= 1;

        }


        if (nextRoom)
        {
            //activate new room
            nextRoom.gameObject.SetActive(true);

            //initialize new room with data from last room
            nextRoom.InitializeRoom(rightAnswer);

            //position camera correctly
            Camera.main.transform.position = cameraTransform.position;
            Camera.main.transform.rotation = cameraTransform.rotation;

            //set player variables and position
            PlayerMovement.playerInstance.setTargets(wayPoints[0].position, wayPoints[1].position, wayPoints[2].position);
            PlayerMovement.playerInstance.transform.position = wayPoints[1].position;

            //disable self
            gameObject.SetActive(false);

        }
        else
        {
            //load ending based on health 
        }
    }
}
