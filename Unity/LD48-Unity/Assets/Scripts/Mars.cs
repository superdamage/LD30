using UnityEngine;
using System.Collections;
using System.Threading;

public class Mars : MonoBehaviour {

	public Sprite[] sprites;
	public Transform tileTemplate;
	public float tileSize = 5;

	public int numTilesX = 4;
	public int numTilesY = 4;

	private int U; //X
	private int V; //Y

	private float spriteSize;

	private float x;
	private float y;
	
	private float genTotal;
	private float gen;

	private Vector2 topLeft = new Vector2();

	public Rect worldBunds;

	public RockGenerator rockGenerator;

	private float SOL_IN_HOURS = (24*1.027f);
	private float SOL_IN_SECONDS;
	public float timeScale = 1;
	public float secondsSpent = 0;
	public float secondsUntilNextSol = 0;
	public float hoursUntilNextSol = 0;
	public float percentTimeLeftForNextSol;
	public int currentSol = 0;
	public float darkness = 0;
	public bool isEarthVisible = false;

	public RoverScreen roverScreen;

	public GUIText solCounterText;

	public Rover rover;

	private float timeOffset = 0;

	private float timeSceneEntered = 0;

	private float lastTimeSolCounterDisplayed = 0;
	private float solCounterDuration = 3.0f;

	public Earth earth;


	void Start () {

		timeSceneEntered = Time.time;

		SOL_IN_SECONDS = SOL_IN_HOURS * 60 * 60;

		// time offset 
		//timeOffset = SOL_IN_SECONDS/4; // daybreak
		timeOffset = 0.0f;

		worldBunds = new Rect();

		U = numTilesX; 
		V = numTilesY;

		genTotal = U * V;
		gen = genTotal;
		
		spriteSize = sprites [0].bounds.size.x * tileSize;
		
		Vector2 finalSize = new Vector2 ();
		finalSize.x = spriteSize * (numTilesX-1);
		finalSize.y = spriteSize * (numTilesY-1);
		
		topLeft.x = -(finalSize.x / 2);
		topLeft.y = -(finalSize.y / 2);

		x = topLeft.x;
		y = topLeft.y;

		worldBunds.x = x;
		worldBunds.y = y;
		worldBunds.width = finalSize.x + spriteSize;
		worldBunds.height = finalSize.y + spriteSize;

		//ThreadStart job = new ThreadStart (GenerateJob);
		//Thread thread = new Thread (job);
		//thread.Start ();

		GenerateJob ();

		rockGenerator.Generate(worldBunds);
	}

	void GenerateJob(){

		while (V>0) {
			
			while (U>0) {
				
				Vector2 pos = new Vector3 (x, y, 0);
				Transform tile = Instantiate (tileTemplate, pos, Quaternion.identity) as Transform;
				//Debug.Log(100-(gen/genTotal*100));
				gen--;
				
				
				tile.parent = this.gameObject.transform;
				SpriteRenderer sr = tile.GetComponent ("SpriteRenderer") as SpriteRenderer;
				
				int randomIndex = (int)Mathf.Round(Random.Range (0, sprites.Length));
				sr.sprite = sprites [randomIndex];
				
				tile.localScale = new Vector3 (tileSize, tileSize, 1);
				
				x += spriteSize;
				
				U--;

			}
			
			U = numTilesX;
			x = topLeft.x;
			y += spriteSize;
			V--;

		}

	}
	
	void Update () {

		// when game resets, time adds up so i had to subtract scene starting time
		float relativeTime = Time.time - timeSceneEntered;

		int oldSol = currentSol;

		secondsSpent = timeOffset + relativeTime * timeScale;
		percentTimeLeftForNextSol = 1 - (secondsSpent % SOL_IN_SECONDS) / SOL_IN_SECONDS;
		secondsUntilNextSol = SOL_IN_SECONDS * percentTimeLeftForNextSol;
		hoursUntilNextSol = SOL_IN_HOURS * percentTimeLeftForNextSol;
		currentSol = (int)Mathf.Ceil(secondsSpent / SOL_IN_SECONDS);

		solCounterText.text = "SOL "+currentSol+"\n"+roverScreen.lifeSupport+" LEFT";


		darkness = Mathf.Abs(((1 - percentTimeLeftForNextSol) - 0.5f) / 0.5f);
		isEarthVisible = darkness <= 0.5;

		if (oldSol != currentSol) {
			newSol();
		}

		if (Time.time > lastTimeSolCounterDisplayed + solCounterDuration) {
			solCounterText.enabled = false;
		}
	}

	void newSol(){

		//Color c = solCounterText.color;
		//c.a = 1;
		solCounterText.enabled = true;

		lastTimeSolCounterDisplayed = Time.time;

		earth.newSol ();
	}


}
