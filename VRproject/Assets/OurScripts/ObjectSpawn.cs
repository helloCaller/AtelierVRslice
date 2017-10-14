using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour {


	public Transform SpawnPrefab;

	GameObject controller;
	Collision collision;

	bool spawn = false;
	Vector3 controllerPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	void OnCollisionEnter(Collision collision)
	{    
		
		if(spawn == false && collision.collider.gameObject.name == "handLeft" | spawn == false && collision.collider.gameObject.name == "handRight")        
		{
			spawn = true;

			controller = GameObject.Find (collision.collider.gameObject.name);
			Debug.Log (controller.transform.position);
			controllerPosition = new Vector3 (controller.transform.position.x,controller.transform.position.y,controller.transform.position.z);
			Instantiate(SpawnPrefab,controllerPosition, Quaternion.identity);

		} else {
			spawn = false;
		}
	}
}
