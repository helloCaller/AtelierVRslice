using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowLights : MonoBehaviour {

	bool LightsON = false;
	public Shader shader1;

	public Renderer rend;
	float gradual = 1.00f;
	float random;
	float wait = 0;

	Material[] mats;
	Material window;

	Color lerpedColour;

	Color lightsOn = Color.yellow;
	Color lightsOff = Color.black;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		Material[] mats = rend.materials;


		random = Random.Range (0.0f, 5.0f);

		foreach (Material matt in mats) {
			if (matt.name == "Window (Instance)") {
				window = matt;
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		
		if (LightsON) {


				wait += 0.01f;
			Debug.Log (wait);

			if (wait >= random) {
				if (gradual >= 0.08f) {
					gradual -= 0.001f;
				}

				lerpedColour = Color.Lerp (Color.yellow, Color.black, gradual);
				Debug.Log ("!!!!!!!!!" + gradual);
				window.SetColor ("_EmissionColor", lerpedColour);
			}
			
			} else {
				if (gradual <= 1) {
					gradual += 0.001f;
				}

				lerpedColour = Color.Lerp (Color.yellow, Color.black, gradual);


				window.SetColor ("_EmissionColor", lerpedColour);
			}
		
	}

	void LightsOn(){
		LightsON = true;

	}

	void LightsOff(){
		LightsON = false;
		wait = 0;
	}
}
