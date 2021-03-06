﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoverScreen : MonoBehaviour {

	private GUITexture background;
	public GUIText screenGUIText;

	//public string question = "";

	private float maxCharsPerLine = 31;

	public int startingLifeSupport = 10;
	public int lifeSupport;

	private int numLineCapacity = 12;

	public LevelLoader levelLoader;

	public Mars mars;
	public Earth earth;

	private bool on = false;

	public GUIText visibilityIndicator;

	//private bool powerOn;
	// Use this for initialization
	void Start () {

		background = GetComponent<GUITexture> ();

		startingLifeSupport += 1; // for past day (spends immediately)

		lifeSupport = startingLifeSupport;

		printScreen ();

	}

	string printScreen(){

		string text = "EARTH:\n";
		text += "------\n";
		text += levelLoader.currentQuestion;

		string linedText = "";
		int numChars = text.Length;
		int charIndex = 0;
		int charsInLine = 0;
		int lineCount = 3;

		while (numChars>0) {

			if(charsInLine>=maxCharsPerLine){
				linedText += "\n";
				charsInLine = 0;
				lineCount++;
			}
			linedText += text[charIndex];

			charIndex++;
			charsInLine++;
			numChars--;
		}

		int remainingLines = numLineCapacity - lineCount;

		while (remainingLines>0) {
			linedText += "\n";
			remainingLines--;
		}

		// print print seperator
		linedText += "\n_______________________________";

		linedText += "\n"; // gap

		// print life support
		string stringLifeSupport = "" + lifeSupport;
		if(stringLifeSupport.Length<2)stringLifeSupport = "0"+stringLifeSupport;
		linedText += "\nLIFE SUPPORT LEFT:      " +stringLifeSupport+" SOLS";

		// print life support
		string d1 = ""+(int)mars.hoursUntilNextSol;
		string d2 = ""+(int)((mars.secondsUntilNextSol/60)%60);
		if (d1.Length < 2)d1 = "0" + d1;
		if (d2.Length < 2)d2 = "0" + d2;
		linedText += "\nHOURS UNTIL NEXT SOL:     " +d1+":"+d2;

		// satelite imagery coverage
		linedText += "\nSATELITE IMAGERY COVERAGE:  "+ (mars.isEarthVisible?"YES":" NO");
		visibilityIndicator.enabled = !mars.isEarthVisible;

		string formattedDarkess = ""+ Mathf.Round((1-(mars.darkness)+0.5f)*100);
		if (formattedDarkess.Length < 2)d1 = "0" + formattedDarkess;
		visibilityIndicator.text = "VISIBILITY IS TOO LOW\n"+formattedDarkess+"%, WAIT FOR SOL";

		return linedText;
	}
	
	// Update is called once per frame
	void Update () {
		string cleanText = printScreen ();
		string glitchedText = cleanText;
		screenGUIText.text = glitchedText;

		//Debug.Log ("cs "+mars.currentSol);
		lifeSupport = startingLifeSupport - mars.currentSol;
		if (lifeSupport <= -1) { // dies when next hungry not when it diminishes
			
			SceneManager.LoadScene ("Death");
		}
	}

	public void setOn(bool _on){

		if (on == _on)return;

		on = _on;
		background.enabled = on;
		screenGUIText.enabled = on;

		if (on) {
			earth.playerIsInRover();
		}
	}

}
