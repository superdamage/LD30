using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Sprite darkSprite;
	public Sprite lightSprite;

	private AudioSource dragSFXSource;

	private float sfxMinVelocity = 0.2f;

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
		dragSFXSource = this.GetComponent("AudioSource") as AudioSource;
	}


	
	// Update is called once per frame
	void Update () {

		if (Mathf.Abs (rigidbody2D.velocity.x) >= sfxMinVelocity ||
		    Mathf.Abs (rigidbody2D.velocity.y) >= sfxMinVelocity) {
		
			dragSFXSource.enabled = true;
		}else{
			dragSFXSource.enabled = false;
		}
	}
}
