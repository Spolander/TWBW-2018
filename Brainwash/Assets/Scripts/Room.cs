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

    public Transform CameraTransform { get { return cameraTransform; } }

    [SerializeField]
    private AudioSource questionSound;

    [SerializeField]
    private AudioSource rightAnswer;

    [SerializeField]
    private AudioSource wrongAnswer;

    [SerializeField]
    private AudioSource inVoiceSpecial;

    [SerializeField]
    private bool actionMusicEnabled = false;

    public static bool goodEndingAchieved= false;

    public void InitializeRoom(bool lastAnwserRight)
    {
        if (questionSound != null)
        {
            if (inVoiceSpecial)
            {
                inVoiceSpecial.Play();
                questionSound.PlayDelayed(inVoiceSpecial.clip.length);
                StartCoroutine(playerWaitTime(questionSound.clip.length + inVoiceSpecial.clip.length));
            }
            else
            {
                questionSound.Play();
                StartCoroutine(playerWaitTime(questionSound.clip.length));
            }

        }
        else
            PlayerMovement.playerInstance.CanMove = true;

        if (actionMusicEnabled)
            actionMusic.instance.MusicState(true);

        PlayerMovement.playerInstance.setTargets(wayPoints[0].position, wayPoints[1].position, wayPoints[2].position);
    }
    //player calls this when near a chosen door
    public void DoorReached(bool rightAnswer, bool killTarget, GameObject door)
    {

        float answerDelay = 0.5f;
        //väärä vastaus
        if (rightAnswer == false)
        {
            soundEngine.instance.FailSound();
            damageFlash.instance.Flash();
            Player.hitPoints -= 1;
            print(Player.hitPoints);
            print("WRONG ANSWER BUDDY");

            if (Player.hitPoints <= 0)
            {

                if (this.wrongAnswer)
                {
                    this.wrongAnswer.Play();
                    StartCoroutine(gameOverAnimation(false, this.wrongAnswer.clip.length));
                }
          
                
                else
                    StartCoroutine(gameOverAnimation(false, 0));

                return;
            }
            else
            {
                if (wrongAnswer && this.wrongAnswer)
                {
                    wrongAnswer.Play();
                    answerDelay = wrongAnswer.clip.length;
                }
            }

        }

        //oikea vastaus
        else
        {
            soundEngine.instance.SuccessSound();
            if(this.rightAnswer)
            {
                if (door.name == "raha")
                    this.wrongAnswer.Play();
                else
                this.rightAnswer.Play();
                answerDelay = this.rightAnswer.clip.length;
            }
        
            if (Player.hitPoints <= 0)
            {
                //good ending
                StartCoroutine(gameOverAnimation(true, 0));
                return;
            }
        }


      

        if (nextRoom)
        {

                ScreenFader.instance.Fade(answerDelay, 0.5f,false);
                StartCoroutine(loadNextRoom(answerDelay,rightAnswer));
            
          

        }
        
    }


    IEnumerator loadNextRoom(float delay, bool rightAnswer)
    {
        yield return new WaitForSeconds(delay + 1);
  
      

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
  

    IEnumerator gameOverAnimation(bool goodEnding, float delay)
    {
        Cursor.visible = false;
        yield return new WaitForSeconds(delay);
        goodEndingAchieved = goodEnding;
        ScreenFader.instance.Fade(0, 0.5f, true);
    }

    IEnumerator playerWaitTime(float time)
    {
        PlayerMovement.playerInstance.CanMove = false;
        yield return new WaitForSeconds(time);
        PlayerMovement.playerInstance.CanMove = true;
    }
}
