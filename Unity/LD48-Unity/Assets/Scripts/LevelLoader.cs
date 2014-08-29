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

	public Transform letterContainer;
	//private Vector3 letterContainerInitialPos;
	public Transform letterPrefab;

	private bool _lettersHidden = true;

	private List<Transform> decodedLines = new List<Transform>();

	//decoded
	/*
	private int decoded_wordIndex = 0;
	private int decoded_letterIndex = 0;
	private int decoded_letterSize = 0;
	private int decoded_totalChars = 0;
	private float decoded_totalLetterX = 0;
	*/
	//private List<Transform> decoded_letters = new List<Transform> ();

	public string currentQuestion = "";
	private string currentAnswer = "";




	void Start () {
		morse = new Morse (morseTextAsset.text);
		//nextLevel ();
		//letterContainerInitialPos = letterContainer.transform.position;
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

		// clear decoded letters
		/*
		decoded_wordIndex = 0;
		decoded_letterIndex = 0;
		decoded_letterSize = 0;
		decoded_totalChars = 0;
		decoded_totalLetterX = 0;

		foreach(Transform letterTransform in decoded_letters ){
			Destroy(letterTransform.gameObject);
		}

		decoded_letters = new List<Transform> ();
		letterContainer.transform.position = letterContainerInitialPos;
		*/
	}

	bool renderLevel(int levelIndex){ // returns true if there are more levels

		string jsonString = levelsAsset.text;
		List<JSONObject> levelDatas = new JSONObject (jsonString).list;
		lastLevelIndex = levelDatas.Count;
		if (levelIndex >= lastLevelIndex)return false; // no more levels

		JSONObject levelData = levelDatas [levelIndex];
		string q = levelData["question"].str;
		string a = levelData["decoded"].str;
		currentAnswer = a;
		string decoded_answer = levelData["decoded"].str;

		Debug.Log ("rendering level: "+levelIndex);
		Debug.Log ("question: "+q);
		Debug.Log ("decoded answer: "+a);

		currentQuestion = q;

		string debugEncoedWords = "";

		printDecodedAnswerLetters (a);

		if (decoded_answer.Length <= 0)return true;

		int[][] encoded_answer = morse.encode (decoded_answer);

		beginNewSentence (encoded_answer.Length);

		//int decodedLetterIndex = 0;

		for (int w=0; w<encoded_answer.Length; w++) {

			int numCodes = encoded_answer[w].Length;

			string debugEncoedWord = "";

			beginNewLine(w,numCodes);

			for (int l=0; l<numCodes; l++) {

				//Debug.Log("LT "+a[w]);

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

		//addLastLetter ();


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

		//setDecodedNewLine ();
	}

	void putPlaceholder(int code){
		Vector3 linePos = linePosStart;
		linePos.x += U*gap.x;
		U++;

		bool isGap = code == morse.gap;

		//setDecodedNextLetter(isGap);

		if (isGap)return;

		Transform placeholder = Instantiate (placeholderPrefab, linePos, Quaternion.identity) as Transform;
		placeholder.parent = this.gameObject.transform;
		RockPlaceholder ph = placeholder.GetComponent("RockPlaceholder") as RockPlaceholder;
		ph.rockDragger = rockDragger;
		ph.setDash(code==morse.dash);

		placeholders.Add (ph);

	}


	/*
	private void setDecodedNextLetter(bool ended){

		decoded_letterSize++;

		if(ended){
			char c = currentAnswer[decoded_letterIndex];
			putLetter(c,decoded_letterSize-1);
			decoded_letterSize =0;
			decoded_letterIndex++;
		}

	}

	private void setDecodedNewLine(){
		decoded_wordIndex++;
		//decoded_letterIndex = 0;
		decoded_totalLetterX = 0;
	}

	private void addLastLetter(){
		setDecodedNextLetter (true);

		Vector3 pos = letterContainer.position;
		pos.x = -sentenceSize.x / 2;
		pos.y += (gap.y)/2;
		letterContainer.position = pos;
	}

	private void putLetter(char l,int size){

		Debug.Log (decoded_totalLetterX);

		Debug.Log ("l "+l+
		           " u:"+decoded_letterIndex+
		           " v: "+decoded_wordIndex+
		           " s"+decoded_letterSize);

		Vector3 letterPos = letterContainer.transform.position;

		//float letterSize = 1;
		float letterWidth = (decoded_letterSize-1)*gap.x;

		Transform letterTransform = Instantiate (letterPrefab, letterPos, Quaternion.identity) as Transform;
		letterTransform.parent = letterContainer;
		TextMesh tm = letterTransform.GetComponent<TextMesh>() as TextMesh;
		tm.text = ""+l;
		letterPos.x = decoded_totalLetterX + (letterWidth/2);
		letterPos.y = - decoded_wordIndex * gap.y;
		letterTransform.position = letterPos;

		decoded_letters.Add (letterTransform);

		decoded_totalLetterX += (letterWidth + gap.x);

		decoded_totalChars++;
	}

	*/


	private void printDecodedAnswerLetters(string decoded){

		foreach(Transform lineTransform in decodedLines){
			Destroy(lineTransform.gameObject);
		}
		decodedLines = new List<Transform> ();

		string[] lines = decoded.Split (' ');

		float yStart = (gap.y * lines.Length)/2 + (-1.394218f);

		Vector3 letterPos = letterContainer.transform.position;
		letterPos.y = yStart;

		foreach (string line in lines) {

			Debug.Log("loop");

			//float xStart = ((line.Length + (line.Length - 1)) * gap.x) / 2;
			//letterPos.x = xStart;

			Transform letterTransform = Instantiate (letterPrefab, letterPos, Quaternion.identity) as Transform;
			TextMesh tm = letterTransform.GetComponent<TextMesh>() as TextMesh;
			decodedLines.Add(letterTransform);
			// add gaps to letters
			tm.text = "";
			foreach(char c in line){

				Debug.Log("loop2");

				tm.text+=(c+" ");
			}

			letterTransform.parent = letterContainer;

			letterPos.y -= gap.y;

		}
	}

	public void setDecodedLettersHidden(bool hidden){
		if (_lettersHidden == hidden)return;
		_lettersHidden = hidden;

		foreach(MeshRenderer mr in letterContainer.GetComponentsInChildren<MeshRenderer>()){
			mr.enabled = !hidden;
		}

		float alpha = this.check()?0.5f:0.06f;


		foreach(TextMesh tm in letterContainer.GetComponentsInChildren<TextMesh>()){
			Color c = tm.color;
			c.a = alpha;
			tm.color = c;
		}

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

		return correct;
	}

	void Update () {

		if (Input.GetKeyDown ("c")) {
			cheat();
		}

	}

	void cheat() {

		for(int p=0; p<placeholders.Count; p++){

			RockPlaceholder rp = placeholders[p];

			if(rp.isCorrect())continue;

			Debug.Log("rp "+rockGenerator);
			Rock r = rockGenerator.addRock(rp.isDash());
			Vector3 pos = rp.transform.position;
			pos.z = r.transform.position.z;
			r.transform.position = pos;
		}

	}

}
