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

	public Mars mars;

	//private bool powerOn;
	// Use this for initialization
	void Start () {

		background = GetComponent<GUITexture> ();

		startingLifeSupport += 1; // for past day (spends immediately)

		lifeSupport = startingLifeSupport;

		printScreen ();

	}

	string printScreen(){

		string text = "EARTH:\n"+levelLoader.currentQuestion;

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

		Debug.Log ("cs "+mars.currentSol);
		lifeSupport = startingLifeSupport - mars.currentSol;
		if (lifeSupport <= 0) {
			Application.LoadLevel("Death");
		}
	}

	public void setOn(bool _on){

		background.enabled = _on;
		screenGUIText.enabled = _on;
	}

}
