using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {



	public GameObject apartmentBlockOne;
	public GameObject apartmentBlockTwo;
	public GameObject apartmentBlockThree;
	public GameObject houseBlock;
	public GameObject personModel;
	public GameObject roofBlock;

<<<<<<< HEAD
	public void ApartmentOne(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(apartmentBlockOne, objectPos, Quaternion.identity);
=======
	public GameObject controller; 

	public void ApartmentOne(){

		/*var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);*/

		Instantiate(apartmentBlockOne, controller.transform.position , Quaternion.identity);
>>>>>>> origin/master
			
	}

	public void ApartmentTwo(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(apartmentBlockTwo, objectPos, Quaternion.identity);

	}

	public void ApartmentThree(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(apartmentBlockThree, objectPos, Quaternion.identity);

	}

	public void House(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(houseBlock, objectPos, Quaternion.identity);

	}

	public void Person(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(personModel, objectPos, Quaternion.identity);

	}

	public void Roof(){

		var mousePos = Input.mousePosition;
		mousePos.z = 300;

		var objectPos = Camera.main.ScreenToWorldPoint(mousePos);

		Instantiate(roofBlock, objectPos, Quaternion.identity);

	}


}

