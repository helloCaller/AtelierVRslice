using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderingNPC : MonoBehaviour {

	float xMove;
	float zMove;
	int stepsTaken = 0;
	int stepCounter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(stepsTaken == 0) {
			xMove = Random.Range(-5f, 5f);
			zMove = Random.Range(-5f, 5f);
			stepCounter = Mathf.RoundToInt(Random.Range(5, 100));
		}

		if (stepsTaken != stepCounter) {
			transform.position = new Vector3 (transform.position.x + (xMove * 0.01f/2), transform.position.y + 0, transform.position.z + (zMove* 0.01f/2));
			stepsTaken += 1;
		} else {
			stepsTaken = 0;
		}
			
	}
}
