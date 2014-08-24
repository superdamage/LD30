using UnityEngine;
using System.Collections;

public class RockDragger : MonoBehaviour {

	public Collider2D pullCandidate;
	public DistanceJoint2D activeJoint = null;

	public AudioSource strainSFX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {

			if (pullCandidate != null && activeJoint == null) {

				DistanceJoint2D dst = pullCandidate.GetComponent<DistanceJoint2D>();

				dst.connectedBody = this.rigidbody2D;
				activeJoint = dst;
				dst.enabled = true;

				strainSFX.Play();
			}

		}

		if (Input.GetKeyUp ("space")) {
			if(activeJoint){
				activeJoint.enabled = false;
				activeJoint.connectedBody = null;
				activeJoint = null;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D o){
		Rock r = o.GetComponent<Rock> ();
		if (r != null) {
			pullCandidate = o;
		}

	}

	void OnTriggerExit2D(Collider2D o){

		if(pullCandidate == o){
			pullCandidate = null;
		}

	}




}
