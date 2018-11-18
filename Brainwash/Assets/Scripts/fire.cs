using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GetComponent<Animator>().Play("fireAnimation", 0, Random.value);
	}
	
}
