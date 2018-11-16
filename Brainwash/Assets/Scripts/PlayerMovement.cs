using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


	Vector3 []targets;
	int target = 1;
	int last = 1;

	public float speed;

	private float step;


	// Use this for initialization
	void Start () {
		targets = new Vector3[3];

	}
	
	// Update is called once per frame
	void Update () {

		step = speed * Time.deltaTime;

		if (target != 1) {

			if (last == 1) {
				
				transform.position = Vector3.MoveTowards (transform.position, targets [target], step);
			} else {
				transform.position = Vector3.MoveTowards (transform.position, targets [1], step);
				if (Vector3.Distance(transform.position, targets[1]) < 0.05f) {
					last = 1;
				}
			}
		}

	}

	public void setTargets(Vector3 target0, Vector3 target1, Vector3 target2){
		targets [0] = target0;
		targets [1] = target1;
		targets [2] = target2;
	}

	public void setMovement(int index){
		
		last = target;
		target = index;
	}

}
