﻿using UnityEngine;
using System.Collections;

public class Rover : MonoBehaviour {

	public SpriteRenderer roof;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D o){

		if (o.tag == "Player" && o.isTrigger==false) {
			Debug.Log("pl");
			//focusSwitcher.playerIsInRover = true;
			roof.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D o){

		if (o.tag == "Player" && o.isTrigger==false) {
			Debug.Log("pl-e");
			//focusSwitcher.playerIsInRover = false;
			roof.enabled = false;
		}
	}
}
