﻿using UnityEngine;
using System.Collections;

public class PlatfromBreak : MonoBehaviour {
	public Animator platformAnimator;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D collider) {
		print ("trigger");
		platformAnimator.enabled = true;
	}
}
