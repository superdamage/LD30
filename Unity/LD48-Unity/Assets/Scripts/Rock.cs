using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Sprite darkSprite;
	public Sprite lightSprite;

	private bool dark = false;
	public bool Dark{
		get{
			return this.dark;
		}
		set{
			SpriteRenderer spriteRenderer = GetComponent(@"SpriteRenderer") as SpriteRenderer;
			spriteRenderer.sprite = value?darkSprite:lightSprite;
			this.dark = value;
		}
	}


	// Use this for initialization
	void Start () {
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
