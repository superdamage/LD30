using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	public Mars mars;

	public LevelLoader levelLoader;

	public GUITexture couldNotDecode;
	public GUITexture couldDecode;
	public GUITexture newMessage;

	private float tryToDecodeTime = Mathf.Infinity;

	private float telemetryDelay = 4.0f;

	private bool willLoadNewLevel = false;


	bool decoding = false;

	// Use this for initialization
	void Start () {
	
	}

	public void newSol(){
		tryToDecodeTime = Time.time + telemetryDelay;

	}

	private void tryToDecode(){
		tryToDecodeTime = Mathf.Infinity;
		decoding = true;


	}
	
	// Update is called once per frame
	void Update () {

		float t = Time.time;

		if (t >= tryToDecodeTime)tryToDecode();

		if (mars.isEarthVisible == false){ 
			Debug.Log("night");
			decoding = false;
		}

		if (decoding) {
			decode ();
		}else{
			Debug.Log("not decoding");
		}

		if (Input.GetKeyDown ("space")) {
			newMessage.enabled = false;
			couldDecode.enabled = false;
			newMessage.enabled = false;
			Time.timeScale = 1.0f; // resume
		}
	}

	void decode(){
		if (levelLoader.currentLevel < 0) {
			onNewMessage();
			decoding = false;
		}else{
			bool decoded = levelLoader.check();
			Debug.Log("Decode: "+decoded);
		}
	}

	void onNewMessage(){
		//newMessage.enabled = true;
		showCutscene (newMessage);
		willLoadNewLevel = true;
	}

	void showCutscene(GUITexture newMessage){
		newMessage.enabled = true;
		Time.timeScale = 0.0f; // pause
	}

	public void playerIsInRover(){
		if (willLoadNewLevel) { // test level progression by uncommenting this
			levelLoader.nextLevel ();
			decoding = true;
			willLoadNewLevel = false;
		}

	}
}
