using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				
				if (hit.transform.name == "target0") {
					
					GameObject.FindObjectOfType<PlayerMovement>().setMovement(0);
				} else if (hit.transform.name == "target2") {
					GameObject.FindObjectOfType<PlayerMovement>().setMovement(2);
				}
			}
		}
	}
}
