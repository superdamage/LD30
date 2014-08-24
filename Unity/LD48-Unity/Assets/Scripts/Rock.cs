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

		float hVelocity = rigidbody2D.velocity.x;
		float vVelocity = rigidbody2D.velocity.y;

		if (Mathf.Abs (hVelocity) >= sfxMinVelocity ||
		    Mathf.Abs (vVelocity) >= sfxMinVelocity) {
		
			dragSFXSource.enabled = true;
			float t = hVelocity;
			if(Mathf.Abs(vVelocity)>Mathf.Abs(hVelocity)){
				t = -vVelocity;
			}

			//t = Mathf.Max(hVelocity,vVelocity);
			rigidbody2D.AddTorque(-t*10);

		}else{
			dragSFXSource.enabled = false;
		}

		Vector3 shadowPosition = this.transform.position;
		shadowPosition.y -= 0.3f;
		shadowPosition.z = -0.6f;
		shadow.position = shadowPosition;
	}
}
