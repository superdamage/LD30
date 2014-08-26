﻿using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	public Mars mars;

	public LevelLoader levelLoader;

	public SpriteRenderer couldNotDecode;
	public SpriteRenderer couldDecode;
	public SpriteRenderer newMessage;

	private float didDecodeMark = Mathf.Infinity;

	private bool earthWillRespond = false;

	//private float tryToDecodeTime = Mathf.Infinity;

	private float telemetryDelay = 2.0f;

	private bool willLoadNewLevel = false; // should enter rover, wont decode as message is old



	private float solMark = Mathf.Infinity;

	// Use this for initialization
	void Start () {
	
	}

	public void newSol(){

		solMark = Time.time;
	}

	void Update () {

		float t = Time.time;

		// can decode
		if(t>solMark+telemetryDelay && mars.isEarthVisible && willLoadNewLevel == false && earthWillRespond == false){
			decode();
		}else{
			//Debug.Log("not decoding");
		}

		if (t > didDecodeMark + telemetryDelay && willLoadNewLevel == false) {
			didDecodeMark = Mathf.Infinity;
			onNewMessage();
		}

		if (Input.GetKeyDown ("space")) {
			newMessage.enabled = false;
			couldDecode.enabled = false;
			newMessage.enabled = false;
			Time.timeScale = 1.0f; // resume
		}
	}

	void decode(){

		bool decoded = levelLoader.check();
		if (decoded && levelLoader.currentLevel < 0) { // first level
			earthWillRespond = true;
			didDecodeMark = Time.time;
			//onNewMessage();
		}else if(decoded == true){
			showCutscene(couldDecode);
			earthWillRespond = true;
			didDecodeMark = Time.time;
		}/*else if(decoded == false){
			showCutscene(couldNotDecode);
		}*/
	}

	void onNewMessage(){
		//newMessage.enabled = true;
		showCutscene (newMessage);
		willLoadNewLevel = true;
		earthWillRespond = false;
	}

	void showCutscene(SpriteRenderer newMessage){
		newMessage.enabled = true;
		Time.timeScale = 0.0f; // pause
	}

	public void playerIsInRover(){
		if (willLoadNewLevel) { // test level progression by uncommenting this
			levelLoader.nextLevel ();
			//decoding = true;
			//Debug.Log("set true2");
			willLoadNewLevel = false;
		}

	}
}
