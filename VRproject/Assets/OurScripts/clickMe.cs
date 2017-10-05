using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickMe : MonoBehaviour {

	public Spawn script; 

	public GameObject apartmentBlockOne;

	/*void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "LeftController" || col.gameObject.name == "RightController") {
			script.ApartmentOne();
		}
	}*/

	public void TEST(){
		script.ApartmentOne();
		Instantiate (apartmentBlockOne);
	}

}
