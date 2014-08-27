using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;


public class LevelLoader : MonoBehaviour {

	public int currentLevel = -1;
	private Morse morse;

	public TextAsset levelsAsset;

	public GUIText visibiltyIndicator;

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

	public RockGenerator rockGenerator;

	private int lastLevelIndex = 0;

	//private float timeMarkJustRendered = 0;
	//private float clearTime = 1;


	public string currentQuestion = "";

	void Start () {
		morse = new Morse (morseTextAsset.text);
		//nextLevel ();
		
	}

	// pass -1 for normal progression
	public bool nextLevel(int forceLevel){
		if (forceLevel < 0) { 
			currentLevel++;
		}else{
			currentLevel = forceLevel;
		}

		destroyCurrentLevel ();
		return renderLevel (currentLevel);
	}

	void destroyCurrentLevel(){

		U = 0;
		V = 0;

		if (placeholders != null) {

			for(int i=0; i<placeholders.Count; i++){
				RockPlaceholder ph = placeholders[i];
				//rockGenerator.replace(ph.snappedRock);
				Destroy(ph.gameObject);
			}
		}

		placeholders = new List<RockPlaceholder> ();
	}

	bool renderLevel(int levelIndex){

		string jsonString = levelsAsset.text;
		List<JSONObject> levelDatas = new JSONObject (jsonString).list;
		lastLevelIndex = levelDatas.Count-1;
		if (levelIndex >= lastLevelIndex)return false; // no more levels

		JSONObject levelData = levelDatas [levelIndex];
		string q = levelData["question"].str;
		string a = levelData["decoded"].str;
		string decoded_answer = levelData["decoded"].str;

		Debug.Log ("rendering level: "+levelIndex);
		Debug.Log ("question: "+q);
		Debug.Log ("decoded answer: "+a);

		currentQuestion = q;

		string debugEncoedWords = "";

		//if (decoded_answer.Length <= 0)return;

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


		return true;

		//timeMarkJustRendered = Time.time;
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

		Debug.Log("nl");

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

		placeholders.Add (ph);

	}

	public bool check(){

		string debug = "";

		if (placeholders.Count <= 0)return false;

		bool correct = true;
		for (int i=0; i<placeholders.Count; i++) {
			RockPlaceholder holder = placeholders[i];

			bool letterCorrect = holder.isCorrect();
			debug += (letterCorrect+",");

			if(holder.isCorrect() == false){
				correct = false;
			}
		}

		Debug.Log(debug);

		return correct;
	}

	void Update () {
		if (Input.GetKeyDown ("c")) {
			cheat();
		}

		/*
		if (Time.time < (timeMarkJustRendered + clearTime)) {
			Debug.Log("cleartime");
		}
		*/
	}

	void cheat() {

		//List<Rock> usedRocks = new List<Rock> ();

		Debug.Log ("phc " + placeholders.Count);

		for(int p=0; p<placeholders.Count; p++){

			RockPlaceholder rp = placeholders[p];

			if(rp.isCorrect())continue;

			Debug.Log("rp "+rockGenerator);
			Rock r = rockGenerator.addRock(rp.isDash());
			Vector3 pos = rp.transform.position;
			pos.z = r.transform.position.z;
			r.transform.position = pos;


			/*

			bool found = false;

			while(found == false){

			for (int i=0; i<rockGenerator.rocks.Count; i++) {
				Rock r = rockGenerator.rocks[i];
			
				if(r!=null && rp!=null ){
					
					if(r.Dark == rp.isDash() ){
				
				if(usedRocks.Contains(r) == false){
					//move
					Vector3 pos = rp.transform.position;
					pos.z = r.transform.position.z;
					r.transform.position = pos;
					
					usedRocks.Add(r);
					
					found = true;
					
					Debug.Log("found");
					
					break;
				}
				
				
				
			}
			
		}
		
	}
	
}

			 */



		}




	}

}
