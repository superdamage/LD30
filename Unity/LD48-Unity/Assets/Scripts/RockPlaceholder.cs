using UnityEngine;
using System.Collections;

public class RockPlaceholder : MonoBehaviour {

	public Sprite dashSprite;
	public Sprite dotSprite;
	public Sprite incorrectSprite;
	public Sprite correctSprite;

	private bool isDash;

	private Rock snappedRock;

	public RockDragger rockDragger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDash(bool _isDash){
		isDash = _isDash;
		setSprite (isDash ? dashSprite : dotSprite);
	}

	void setSprite(Sprite sprite){
		SpriteRenderer spriteRenderer = transform.GetComponent ("SpriteRenderer") as SpriteRenderer;
		spriteRenderer.sprite = sprite;
	}

	void OnTriggerEnter2D(Collider2D o){
		Rock r = o.GetComponent<Rock> ();
		if (r != null) {
			if(r.Dark == isDash){
				snap(r);
				setSprite(correctSprite);
			}else if(snappedRock==null){
				setSprite(incorrectSprite);
			}


		}
		
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
				setDash(isDash);
			}else if(snappedRock==null){
				setDash(isDash);
			}
		}
		
	}


}
