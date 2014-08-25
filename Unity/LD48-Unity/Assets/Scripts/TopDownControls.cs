using UnityEngine;
using System.Collections;

public class TopDownControls : MonoBehaviour{
	// Normal Movements Variables
	private float walkSpeed;
	private float sprintSpeed;
	private float curSpeed;
	private float maxSpeed;

	private float sfxMinVelocity = 0.5f;

	public AudioSource footStepsSFX;

	public Animator animator;

	public Mars mars;

	private RockDragger dragger;
	
	void Start()
	{
		float Agility = 10f;
		float Speed = 10f;
		
		walkSpeed = (float)(Speed + (Agility/5));
		sprintSpeed = walkSpeed + (walkSpeed / 2);

		walkSpeed *= 0.6f;


	}
	
	void FixedUpdate()
	{
		curSpeed = walkSpeed;
		maxSpeed = curSpeed;
		
		// Move senteces
		rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
		                                   Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));

		bool dragging = false;
		dragger = this.GetComponent("RockDragger") as RockDragger;
		if (dragger.activeJoint != null) {
			footStepsSFX.pitch = 0.9f;
			dragging = true;
		} else {
			footStepsSFX.pitch = 1.9f;
		}

		float velocityX = rigidbody2D.velocity.x;
		float velocityY = rigidbody2D.velocity.y;

		if (Mathf.Abs (velocityX) >= sfxMinVelocity ||
		    Mathf.Abs (velocityY) >= sfxMinVelocity) {
			
			footStepsSFX.enabled = true;

			Quaternion r = transform.rotation;
			r.y = (velocityX<=0)?180:0;
			transform.rotation = r;

			animator.Play(dragging?"drag":"run");


		}else{
			footStepsSFX.enabled = false;

			animator.Play("idle");
		}


		float halfW = mars.worldBunds.width / 2 - 0.5f;
		float halfH = mars.worldBunds.height / 2 - 0.8f;
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp (pos.x,-halfW,halfW);
		pos.y = Mathf.Clamp (pos.y,-halfH,halfH);
		this.transform.position = pos;

	}
}