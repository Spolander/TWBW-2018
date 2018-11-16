using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    [SerializeField]
    private LayerMask raycastLayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, raycastLayer)) {

                if (hit.transform.name == "doorLeft")
                {

                    GameObject.FindObjectOfType<PlayerMovement>().setMovement(0);
                }
                else if (hit.transform.name == "doorRight")
                {
                    GameObject.FindObjectOfType<PlayerMovement>().setMovement(2);
                }
                else if (hit.transform.GetComponent<Interactable>())
                {
                    hit.transform.GetComponent<Interactable>().Interact();
                }
			}
		}
	}
}
