using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	public Sprite darkSprite;
	public Sprite lightSprite;
	public Transform rockShadowPrefab;
	private AudioSource dragSFXSource;

	public Transform shadow;

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
		//isDragTarget (false);
	}

	/*
	public void isDragTarget(bool value){
		targetIndicatorSpriteRenderer.enabled = value;
	}
	*/

	/*
	public void setFixed(bool isFixed){
		transform.rigidbody2D.isKinematic = isFixed;
	}
	*/
	
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

	void OnCollisionEnter2D(Collision2D o){
		if (o.gameObject.tag == "Rover") {
			relocate();
		}
		/*
		if ( == "Rover") {
			Debug.Log("rv");
			renderer.enabled = false;
		}
		*/
	}

	void OnCollisionStay2D(Collision2D o){
		if (o.gameObject.tag == "Rover") {
			relocate();
		}
	}

	void OnTriggerStay2D(Collider2D o){
		if (o.tag == "Rover") {
			relocate();
		}
	}

	void OnTriggerEnter2D(Collider2D o){
		if (o.tag == "Rover") {
			relocate();
		}
		/*
		if ( == "Rover") {
			Debug.Log("rv");
			renderer.enabled = false;
		}
		*/
	}

	void relocate(){

		this.transform.rigidbody2D.velocity = Vector2.zero;

		DistanceJoint2D dst = transform.GetComponent<DistanceJoint2D>();
		if (dst.connectedBody!=null) {

			RockDragger rd = dst.connectedBody.transform.GetComponent<RockDragger>();
			rd.activeJoint = null;
			dst.connectedBody = null;
		}

		if (this.shadow) {
			Destroy (this.shadow.gameObject);
		}
		RockGenerator rg = this.transform.parent.GetComponent<RockGenerator> ();
		rg.replace (this);

		//this.transform.position = rg.randomPositon();

		//float forceX = -transform.rigidbody2D.velocity.x*2;
		//float forceY = -transform.rigidbody2D.velocity.y*2;

		//transform.rigidbody2D.AddTorque (-forceX*20);
		//transform.rigidbody2D.AddForce (new Vector2 (forceX,forceY));
		//transform.rigidbody2D.velocity = new Vector2 (forceX,forceY);
	}




}
