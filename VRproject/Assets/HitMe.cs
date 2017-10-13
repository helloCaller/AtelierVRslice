using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMe : MonoBehaviour {

	Collision collision;
	GameObject origami;

	float origamix;
	float origamiy;

	bool crumple = false;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {




		
	}
	void Crumple(){
		crumple = true;
		Debug.Log ("crumple heard");
	}

	void OnCollisionEnter(Collision collision)
	{    
		GameObject.Find ("LeftController").SendMessage ("Collide");
		GameObject.Find ("RightController").SendMessage ("Collide");
		if(collision.collider.gameObject.name != null && crumple == true)        
		{
			Debug.Log (collision.collider.gameObject.name);
			origami = GameObject.Find (collision.collider.gameObject.name);
			origamix = origami.transform.localScale.x;
			origamiy = origami.transform.localScale.y;
			origami.transform.localScale = new Vector3(origamix, origamiy, 0.5f);
			crumple = false;

		}        
	}


}
