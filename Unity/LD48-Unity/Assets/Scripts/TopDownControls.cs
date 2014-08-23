using UnityEngine;
using System.Collections;

public class TopDownControls : MonoBehaviour{
	// Normal Movements Variables
	private float walkSpeed;
	private float sprintSpeed;
	private float curSpeed;
	private float maxSpeed;
	
	void Start()
	{
		float Agility = 10f;
		float Speed = 10f;
		
		walkSpeed = (float)(Speed + (Agility/5));
		sprintSpeed = walkSpeed + (walkSpeed / 2);
		
	}
	
	void FixedUpdate()
	{
		curSpeed = walkSpeed;
		maxSpeed = curSpeed;
		
		// Move senteces
		rigidbody2D.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
		                                   Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));
	}
}