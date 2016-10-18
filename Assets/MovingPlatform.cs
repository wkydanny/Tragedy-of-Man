﻿using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    float movingSpeed;

	// Use this for initialization
	void Start () {
        movingSpeed = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        var tempPos = this.transform.position;
        tempPos.x -= movingSpeed * Time.deltaTime;
        transform.position = tempPos;
    }
}
