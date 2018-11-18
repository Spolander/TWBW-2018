using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField]
    private LayerMask raycastLayer;

    private Camera cam;

    [SerializeField]
    private RenderTexture rendTex;


    public Texture2D defaultCursor;

    public Texture2D interactCursor;

	// Use this for initialization
	void Awake () {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(defaultCursor, new Vector2(defaultCursor.width/2, 0), CursorMode.ForceSoftware);
      
	}
	
	// Update is called once per frame
	void Update () {


        //otetann normalisoidut koordinaatit hiirelle (0-1)
        Vector2 mouseLerp;
        mouseLerp.x = Input.mousePosition.x / Screen.width;
        mouseLerp.y = Input.mousePosition.y / Screen.height;


        //PASKAA KOODIA MUTTA VARMUUDEN VUOKSI JÄTIN
        //Vector2 mousePosition = Input.mousePosition;
        //mousePosition.x = rendTex.width * mouseLerp.x;
        //mousePosition.y = rendTex.height * mouseLerp.y;

        //mousePosition.x *= multiplier;
        //mousePosition.y *= heightMultiplier;


        //mousePosition.x = rendTex.width * mouseLerp.x;
        //mousePosition.x += rendTex.width/2;


        //Ray dray = cam.ViewportPointToRay(new Vector3(mouseLerp.x, mouseLerp.y, 0));
        //Debug.DrawRay(dray.origin, dray.direction.normalized*100);

        Ray checkRay = cam.ViewportPointToRay(new Vector3(mouseLerp.x, mouseLerp.y, 0));
        RaycastHit checkHit;

        if (Physics.Raycast(checkRay, out checkHit, 50f, raycastLayer))
        {
            if (checkHit.transform.tag == "doorLeft" || checkHit.transform.tag == "doorRight" || checkHit.transform.GetComponent<Interactable>())
            {
                Cursor.SetCursor(interactCursor, new Vector2(defaultCursor.width / 2, 0), CursorMode.ForceSoftware);
            }
            else Cursor.SetCursor(defaultCursor, new Vector2(defaultCursor.width / 2, 0), CursorMode.ForceSoftware);

        }
        else
            Cursor.SetCursor(defaultCursor, new Vector2(defaultCursor.width / 2, 0), CursorMode.ForceSoftware);

        if (Input.GetMouseButtonDown (0) && PlayerMovement.playerInstance.CanMove) {

            //raycastataan normalisoiduilla koordinaateilla jotka muuntaa sen screen koordinaatteihin
			Ray ray = cam.ViewportPointToRay(new Vector3(mouseLerp.x, mouseLerp.y, 0));
            RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 50f, raycastLayer)) {

                if (hit.transform.tag == "doorLeft")
                {
                    PlayerMovement.playerInstance.TargetDoor = hit.collider.GetComponent<Door>();
                    PlayerMovement.playerInstance.setMovement(0);
                    PlayerMovement.playerInstance.transform.localScale = new Vector3(-1, 1, 1);
                }
                else if (hit.transform.tag == "doorRight")
                {
                    PlayerMovement.playerInstance.TargetDoor = hit.collider.GetComponent<Door>();
                    PlayerMovement.playerInstance.setMovement(2);
                    PlayerMovement.playerInstance.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (hit.transform.GetComponent<Interactable>())
                {
                    PlayerMovement.playerInstance.setMovement(1);
                    PlayerMovement.playerInstance.CurrentInteractTarget = hit.transform.GetComponent<Interactable>();
                    PlayerMovement.playerInstance.ObjectReached = false;

                    if(PlayerMovement.playerInstance.last == 0)
                        PlayerMovement.playerInstance.transform.localScale = new Vector3(1, 1, 1);
                    else
                        PlayerMovement.playerInstance.transform.localScale = new Vector3(-1, 1, 1);

                }
			}
		}
	}
}
