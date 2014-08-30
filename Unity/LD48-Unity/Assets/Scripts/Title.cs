﻿using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public GUITextButton sdButton;
	public GUITextButton playButton;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
	
		sdButton.OnButtonMouseUp += onOpenSDSite;
		playButton.OnButtonMouseUp += onPlay;
	}

	void onOpenSDSite(){
		Application.OpenURL (@"http://superdamage.com");
	}

	void onPlay(){

		Application.LoadLevel (@"Story1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
