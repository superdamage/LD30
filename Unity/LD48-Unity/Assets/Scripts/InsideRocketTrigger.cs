﻿using UnityEngine;
using System.Collections;

public class InsideRocketTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D o){
		
		if (o.tag == "Player") {
			Debug.Log("rocket triger");

			this.transform.parent.GetComponent<Rocket>().playerJustGitInRocket();
			//this.transform.GetComponent<Rocket> ().playerJustGitInRocket ();
			//focusSwitcher.playerIsInRover = true;
			//roverScreen.setOn(true);
		}
	}
	/*
	void OnTriggerExit2D(Collider2D o){
		
		if (o.tag == "Player") {
			focusSwitcher.playerIsInRover = false;
			roverScreen.setOn(false);
		}
	}
	*/

}
