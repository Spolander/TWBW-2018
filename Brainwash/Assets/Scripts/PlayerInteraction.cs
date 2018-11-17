using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField]
    private LayerMask raycastLayer;

    private Camera cam;

    [SerializeField]
    private RenderTexture rendTex;




	// Use this for initialization
	void Awake () {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;

      
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

		if (Input.GetMouseButtonDown (0)) {

            //raycastataan normalisoiduilla koordinaateilla jotka muuntaa sen screen koordinaatteihin
			Ray ray = cam.ViewportPointToRay(new Vector3(mouseLerp.x, mouseLerp.y, 0));
            RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 50f, raycastLayer)) {

                print(hit.collider.name);
                if (hit.transform.tag == "doorLeft")
                {

                    PlayerMovement.playerInstance.setMovement(0);
                }
                else if (hit.transform.tag == "doorRight")
                {
                    PlayerMovement.playerInstance.setMovement(2);
                }
                else if (hit.transform.GetComponent<Interactable>())
                {
                    hit.transform.GetComponent<Interactable>().Interact();
                }
			}
		}
	}
}
