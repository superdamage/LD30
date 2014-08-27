using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	public Mars mars;

	public LevelLoader levelLoader;

	public SpriteRenderer couldNotDecode;
	public SpriteRenderer couldDecode;
	public SpriteRenderer newMessage;

	public RoverScreen roverScreen;

	private float didDecodeMark = Mathf.Infinity;

	private bool earthWillRespond = false;

	//private float tryToDecodeTime = Mathf.Infinity;

	public float telemetryDelay = 3.5f;

	private bool willLoadNewLevel = false; // should enter rover, wont decode as message is old

	private bool lastLevel = false;

	private float solMark = Mathf.Infinity;

	private bool displayingDecodedMessage = false;
	private float timeMarkBeganDisplayingDecodedMessage = 0;
	private float decodedMessageDisplayDuration = 2.0f;

	public AudioSource roverNewMessageSound;

	public int debugForceNextLevel = -1;

	public SpriteRenderer missionUpdateIndicator;

	public CamerFocusSwitcher focusSwitcher;

	// Use this for initialization
	void Start () {
		Color c = missionUpdateIndicator.color;
		c.a = 0;
		missionUpdateIndicator.color = c;
	}

	public void newSol(){

		solMark = Time.time;
	}

	void Update () {

		float t = Time.time;

		if(displayingDecodedMessage){
			if((timeMarkBeganDisplayingDecodedMessage+decodedMessageDisplayDuration)<t){
				endDisplayingDecodedMessage();
			}else{
				return;
			}

		}

		// can decode
		if(t>solMark+telemetryDelay && mars.isEarthVisible && willLoadNewLevel == false && earthWillRespond == false){
			decode();
		}else{
			//Debug.Log("not decoding");
		}

		if (t > (didDecodeMark + telemetryDelay ) && willLoadNewLevel == false) {
			didDecodeMark = Mathf.Infinity;
			onNewMessage();
		}

		if (Input.GetKeyDown ("space")) {
			newMessage.enabled = false;
			couldDecode.enabled = false;
			newMessage.enabled = false;
			Time.timeScale = 1.0f; // resume
		}

		missionUpdateIndicator.enabled = willLoadNewLevel;
		roverNewMessageSound.volume = willLoadNewLevel ? 1 : 0;
		// animate
		Color c = missionUpdateIndicator.color;
		c.a += Time.deltaTime * 1.2f;
		if(c.a>=1)c.a = 0;
		missionUpdateIndicator.color = c;

	}

	void decode(){

		bool decoded = levelLoader.check();

		if (levelLoader.currentLevel < 0) { // first level
			earthWillRespond = true;
			didDecodeMark = Time.time;
			//onNewMessage();

		}else if (decoded && levelLoader.currentLevel < 0) { // first level
			earthWillRespond = true;
			didDecodeMark = Time.time;
			//onNewMessage();

		}else if(decoded == true){

			didDecodeMark = Time.time;
			earthWillRespond = true;
			beginDisplayingDecodedMessage();

		}/*else if(decoded == false){
			showCutscene(couldNotDecode);
		}*/
	}

	private void beginDisplayingDecodedMessage(){
		displayingDecodedMessage = true;
		timeMarkBeganDisplayingDecodedMessage = Time.time;
		focusSwitcher.forceOverview (true);
	}

	private void endDisplayingDecodedMessage(){

		if(timeMarkBeganDisplayingDecodedMessage<=0) return;

		Debug.Log ("endDisplayingDecodedMessage");

		timeMarkBeganDisplayingDecodedMessage = 0;
		focusSwitcher.forceOverview (false);

		didDecodeMark = Time.time;

		showCutscene(couldDecode);

	}

	public void continueAfterDecodedDisplay(){
		displayingDecodedMessage = false;
	}

	void onNewMessage(){
		//newMessage.enabled = true;
		showCutscene (newMessage);

		willLoadNewLevel = true;
		earthWillRespond = false;
	}

	void showCutscene(SpriteRenderer newMessage){
		newMessage.enabled = true;
		Time.timeScale = 0.0f; // pause
		roverScreen.setOn (false);
	}

	public void playerIsInRover(){
		if (willLoadNewLevel) { // test level progression by uncommenting this

			bool notLastLevel = levelLoader.nextLevel (debugForceNextLevel);
			debugForceNextLevel=-1;

			lastLevel = !notLastLevel;

			Debug.Log("LAST LEVEL"+lastLevel);

			//decoding = true;
			//Debug.Log("set true2");
			willLoadNewLevel = false;
		}else{
			Debug.Log("ELSE");
		}

	}
}
