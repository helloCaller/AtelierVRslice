using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateS2 : MonoBehaviour {

    public float speed = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(speed, 0, 0);

    }
}
