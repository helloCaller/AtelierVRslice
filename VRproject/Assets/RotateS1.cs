﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateS1 : MonoBehaviour {

    public float speed = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, speed, 0);

    }
}
