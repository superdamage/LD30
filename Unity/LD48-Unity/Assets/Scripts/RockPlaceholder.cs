using UnityEngine;
using System.Collections;

public class RockPlaceholder : MonoBehaviour {

	public Sprite dashSprite;
	public Sprite dotSprite;
	public Sprite incorrectSprite;
	public Sprite correctSprite;
	
	private AudioSource correctAudioSource;
	private bool _isDash;

	public Rock snappedRock;

	public RockDragger rockDragger;

	private SpriteRenderer spriteRenderer;

	//public LevelLoader levelLoader;
	public RockGenerator rocks;

	private float markTimeCreated = 0;
	private float clearTime = 0.6f;

	// Use this for initialization
	void Start () {
		markTimeCreated = Time.time;

		correctAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDash(bool dash){
		_isDash = dash;
		setSprite (_isDash ? dashSprite : dotSprite);
	}

	public bool isDash(){
		return _isDash;
	}

	void setSprite(Sprite sprite){
		spriteRenderer = transform.GetComponent ("SpriteRenderer") as SpriteRenderer;
		spriteRenderer.sprite = sprite;
	}

	void OnTriggerEnter2D(Collider2D o){
		Rock r = o.GetComponent<Rock> ();
		if (r != null) {

			if(Time.time<(markTimeCreated+clearTime)){ // clear it

				Debug.Log("clear time");
				r.relocate();
				return;	
			}

			if(r.Dark == _isDash){
				snap(r);
				correctAudioSource.Play();
				setSprite(correctSprite);
			}else if(snappedRock==null){
				setSprite(incorrectSprite);
			}


		}
		
	}


	public bool isCorrect(){
		//SpriteRenderer spriteRenderer = transform.GetComponent ("SpriteRenderer") as SpriteRenderer;
		return spriteRenderer.sprite == correctSprite;
	}

	void snap(Rock r){

		snappedRock = r;

		//rockDragger.releaseIfDragging (r.transform);

		/*
		Vector3 pos = r.transform.position;
		pos.x = transform.position.x;
		pos.y = transform.position.y;
		snappedRock.transform.position = pos;
		snappedRock.transform.rotation = Quaternion.identity;
		//snappedRock.setFixed(true);
		*/
	}

	void OnTriggerExit2D(Collider2D o){

		Rock r = o.GetComponent<Rock> ();
		if (r != null) {
			if(snappedRock == r){
				//snappedRock.setFixed(false);
				snappedRock = null;
				setDash(_isDash);
			}else if(snappedRock==null){
				setDash(_isDash);
			}
		}
		
	}

}
