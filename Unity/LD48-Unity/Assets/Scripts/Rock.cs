using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Sprite darkSprite;
	public Sprite lightSprite;
	public Transform rockShadowPrefab;
	private AudioSource dragSFXSource;

	private Transform shadow;

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
		shadow = Instantiate (rockShadowPrefab, this.transform.position, Quaternion.identity) as Transform;
		shadow.parent = this.transform.parent;
	}


	
	// Update is called once per frame
	void Update () {

		if (Mathf.Abs (rigidbody2D.velocity.x) >= sfxMinVelocity ||
		    Mathf.Abs (rigidbody2D.velocity.y) >= sfxMinVelocity) {
		
			dragSFXSource.enabled = true;
		}else{
			dragSFXSource.enabled = false;
		}

		Vector3 shadowPosition = this.transform.position;
		shadowPosition.y -= 0.3f;
		shadowPosition.z -= -2.0f;
		shadow.position = shadowPosition;
	}
}
