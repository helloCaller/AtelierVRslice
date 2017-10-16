using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMe : MonoBehaviour {

	Collision collision;
	GameObject origami;

	float origamix;
	float origamiy;

	bool crumpleConfirmation = false;
	string myName;

	float LeftRight;
	 

	// Use this for initialization
	void Start () {
		myName = this.name;

		if (myName == "Right") {
			LeftRight = 1.0f;
		} else {
			LeftRight = 0.0f;
		}
			

	}

	// Update is called once per frame
	void Update () {





	}
	void Crumple(){
		crumpleConfirmation = true;
		Debug.Log ("crumple heard");
	}



	void OnCollisionEnter(Collision collision)
	{    
		GameObject.Find (myName + "Controller").SendMessage ("Collide", LeftRight);

		if(collision.collider.gameObject.name != null && crumpleConfirmation && collision.collider.gameObject.name != "Floor" && collision.collider.gameObject.name != "Right" && collision.collider.gameObject.name != "Left")        
		{
			Debug.Log (collision.collider.gameObject.name);
			origami = GameObject.Find (collision.collider.gameObject.name);
			origamix = origami.transform.localScale.x;
			origamiy = origami.transform.localScale.y;
			origami.transform.localScale = new Vector3(origamix, origamiy, 0.5f);
			crumpleConfirmation = false;

		}        
	}


}