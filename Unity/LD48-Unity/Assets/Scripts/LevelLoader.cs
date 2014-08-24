using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


public class LevelLoader : MonoBehaviour {

	public int currentLevel = -1;
	private Morse morse;

	public TextAsset[] levelAssets;
	public TextAsset morseTextAsset;

	void Start () {
		morse = new Morse (morseTextAsset.text);
		nextLevel ();
		
	}

	void nextLevel(){
		currentLevel++;
		destroyCurrentLevel ();
		renderLevel (currentLevel);
	}

	void destroyCurrentLevel(){
		
	}

	void renderLevel(int levelIndex){
		string jsonString = levelAssets [levelIndex].text;
		JSONObject levelData = new JSONObject (jsonString);
		string q = levelData["question"].str;
		string decoded_answer = levelData["decoded"].str;

		string debugEncoedWords = "";

		int[][] encoded_answer = morse.encode (decoded_answer);
		for (int w=0; w<encoded_answer.Length; w++) {

			string debugEncoedWord = "";
			for (int l=0; l<encoded_answer[w].Length; l++) {
				debugEncoedWord += encoded_answer[w][l];
			}
			debugEncoedWords+=debugEncoedWord;
			if(w!=encoded_answer.Length-1){
				debugEncoedWords+=("/");
			}

		}
		Debug.Log ("enc " + debugEncoedWords);
	}

	void Update () {
	
	}
}
