using UnityEngine;
using System.Collections;

public class RockPlaceholder : MonoBehaviour {

	public Sprite dashSprite;
	public Sprite dotSprite;
	public Sprite incorrectSprite;
	public Sprite correctSprite;

	private bool isDash;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setDash(bool _isDash){
		isDash = _isDash;
		SpriteRenderer spriteRenderer = transform.GetComponent ("SpriteRenderer") as SpriteRenderer;
		spriteRenderer.sprite = isDash ? dashSprite : dotSprite;
	}
}
