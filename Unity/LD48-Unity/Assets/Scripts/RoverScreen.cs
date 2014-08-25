using UnityEngine;
using System.Collections;

public class RoverScreen : MonoBehaviour {

	private GUITexture background;
	public GUIText screenGUIText;

	//public string question = "";

	private float maxCharsPerLine = 31;

	public int startingLifeSupport = 10;
	private int lifeSupport;

	private int numLineCapacity = 13;

	public LevelLoader levelLoader;

	//private bool powerOn;
	// Use this for initialization
	void Start () {

		background = GetComponent<GUITexture> ();

		lifeSupport = startingLifeSupport;

		//question = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tristique nec mauris a sagittis. Nam lobortis ornare semper. Mauris feugiat tortor sed turpis dictum, ultrices venenatis nibh tempus.";
		printScreen ();

	}

	string printScreen(){

		string text = "EARTH:\n"+levelLoader.currentQuestion;

		Debug.Log (text);

		string linedText = "";
		int numChars = text.Length;
		int charIndex = 0;
		int charsInLine = 0;
		int lineCount = 2;

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
		linedText += "\n- - - - - - - - - - - - - - - -";

		linedText += "\n"; // gap

		// print life support
		linedText += "\nLIFE SUPPORT LEFT : " +lifeSupport+" SOLS";

		// print life support
		linedText += "\nTIME UNTIL NEXT SOL : " +lifeSupport;

		return linedText;
	}
	
	// Update is called once per frame
	void Update () {
		string cleanText = printScreen ();
		string glitchedText = cleanText;
		screenGUIText.text = glitchedText;
	}

	public void setOn(bool _on){

		background.enabled = _on;
		screenGUIText.enabled = _on;
	}

}
