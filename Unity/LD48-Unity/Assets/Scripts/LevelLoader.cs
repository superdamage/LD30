using UnityEngine;
using System.Collections;


public class LevelLoader : MonoBehaviour {

	public int currentLevel = -1;
	private Morse morse;

	public TextAsset[] levelAssets;
	public TextAsset morseTextAsset;

	void Start () {
		morse = new Morse (morseTextAsset.text);
		string[] test_decoded = morse.encode(@"hello world");
		Debug.Log ("DCD "+test_decoded[0]+" "+test_decoded[1]);
		
	}

	void nextLevel(){
		currentLevel++;
		destroyCurrentLevel ();
		loadLevel (currentLevel);
	}

	void destroyCurrentLevel(){
		
	}

	void loadLevel(int levelIndex){
		string jsonString = levelAssets [levelIndex].text;
		JSONObject levelData = new JSONObject (jsonString);
		string q = levelData["question"].str;
		string a = levelData["decoded"].str;
	}

	void Update () {
	
	}
}
