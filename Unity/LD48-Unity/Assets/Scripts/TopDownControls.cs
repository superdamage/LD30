using UnityEngine;
using System.Collections;

public class TopDownControls : MonoBehaviour{
	// Normal Movements Variables
	private float walkSpeed;
	private float sprintSpeed;
	private float curSpeed;
	private float maxSpeed;

	private float sfxMinVelocity = 0.2f;

	public AudioSource footStepsSFX;

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

		dragger = this.GetComponent("RockDragger") as RockDragger;
		if (dragger.activeJoint != null) {
			footStepsSFX.pitch = 0.9f;
		} else {
			footStepsSFX.pitch = 1.9f;
		}

		if (Mathf.Abs (rigidbody2D.velocity.x) >= sfxMinVelocity ||
		    Mathf.Abs (rigidbody2D.velocity.y) >= sfxMinVelocity) {
			
			footStepsSFX.enabled = true;
		}else{
			footStepsSFX.enabled = false;
		}
	}
}