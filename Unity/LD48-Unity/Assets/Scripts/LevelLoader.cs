using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


public class LevelLoader : MonoBehaviour {

	public int currentLevel = -1;
	private Morse morse;

	public TextAsset levelsAsset;

	//public TextAsset[] levelAssets;
	public TextAsset morseTextAsset;

	private List<RockPlaceholder> placeholders = new List<RockPlaceholder>();

	private float U;
	private float V;
	private Vector3 linePosStart;

	public Transform placeholderPrefab;

	public Vector2 gap = new Vector2(3,3);

	public RockDragger rockDragger;

	public Vector2 sentenceSize = new Vector2();


	public string currentQuestion = "";

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
		U = 0;
		V = 0;

		if (placeholders != null) {
			for(int i=0; i<placeholders.Count; i++){
				Destroy(placeholders[i].gameObject);
			}
		}

		placeholders = new List<RockPlaceholder> ();
	}

	void renderLevel(int levelIndex){

		string jsonString = levelsAsset.text;
		List<JSONObject> levelDatas = new JSONObject (jsonString).list;
		JSONObject levelData = levelDatas [levelIndex];
		string q = levelData["question"].str;
		string a = levelData["decoded"].str;
		string decoded_answer = levelData["decoded"].str;

		Debug.Log ("rendering level: "+levelIndex);
		Debug.Log ("question: "+q);
		Debug.Log ("decoded answer: "+a);

		currentQuestion = q;

		string debugEncoedWords = "";

		int[][] encoded_answer = morse.encode (decoded_answer);

		beginNewSentence (encoded_answer.Length);

		for (int w=0; w<encoded_answer.Length; w++) {

			int numCodes = encoded_answer[w].Length;

			string debugEncoedWord = "";

			beginNewLine(w,numCodes);

			for (int l=0; l<numCodes; l++) {
				int code = encoded_answer[w][l];
				debugEncoedWord += code;
				putPlaceholder(code);
			}
			debugEncoedWords+=debugEncoedWord;
			if(w!=encoded_answer.Length-1){
				debugEncoedWords+=("/");
			}

		}
		Debug.Log ("dec:"+decoded_answer+" enc:"+ debugEncoedWords);
	}

	void beginNewSentence(int numLines){
	
		Debug.Log ("nl "+numLines);

		float sentenceHeight = (numLines+1) * gap.y;

		sentenceSize.y = sentenceHeight;
		sentenceSize.x = 0;

		linePosStart.y = sentenceHeight / 2;
		linePosStart.z = this.transform.position.z;
	}

	void beginNewLine(int line,int numCodes){
		U = 0;
		V++;

		float lineWidth = gap.x * numCodes;

		sentenceSize.x = Mathf.Max (sentenceSize.x,lineWidth);

		linePosStart.x = (-lineWidth / 2) + gap.x/2;
		linePosStart.y -= gap.y;
	}

	void putPlaceholder(int code){
		Vector3 linePos = linePosStart;
		linePos.x += U*gap.x;
		U++;

		if (code == morse.gap)return;

		Transform placeholder = Instantiate (placeholderPrefab, linePos, Quaternion.identity) as Transform;
		placeholder.parent = this.gameObject.transform;
		RockPlaceholder ph = placeholder.GetComponent("RockPlaceholder") as RockPlaceholder;
		ph.rockDragger = rockDragger;
		ph.setDash(code==morse.dash);	

	}

	void Update () {
	
	}
}
