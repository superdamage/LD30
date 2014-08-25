using UnityEngine;
using System.Collections;

public class RoverScreen : MonoBehaviour {

	private GUITexture background;
	public GUIText screenGUIText;

	public string question = "";

	private float maxCharsPerLine = 31;
	//private bool powerOn;
	// Use this for initialization
	void Start () {

		background = GetComponent<GUITexture> ();

		question = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tristique nec mauris a sagittis. Nam lobortis ornare semper. Mauris feugiat tortor sed turpis dictum, ultrices venenatis nibh tempus.";
		printScreen ();

	}

	void printScreen(){

		// print question
		string text = "EARTH: "+question;

		Debug.Log (text);

		string linedText = "";
		int numChars = text.Length;
		int charIndex = 0;
		int charsInLine = 0;
		while (numChars>0) {

			if(charsInLine>=maxCharsPerLine){
				linedText += "\n";
				charsInLine = 0;
			}
			linedText += text[charIndex];

			charIndex++;
			charsInLine++;
			numChars--;
		}
		screenGUIText.text = linedText;

		// print print seperator
		screenGUIText.text += "\n-------------------------------\n";

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setOn(bool _on){
		background.enabled = _on;
	}
	/*
	public void setPowerOn(bool _powerOn){
		//if (_powerOn == powerOn)return;
		_powerOn = powerOn;

		background.enabled = powerOn;
	}
	*/
}
