using UnityEngine;
using System.Collections;

public class RockDragger : MonoBehaviour {

	public Collider2D pullCandidate;
	public DistanceJoint2D activeJoint = null;

	public AudioSource strainSFX;

	public SnapToTarget dragTargetIndicator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {

			if (pullCandidate != null && activeJoint == null) {

				startPulling();
			}

		}

		if (Input.GetKeyUp ("space")) {
			if(activeJoint){
				release();
			}
		}

		if (activeJoint != null) {
			dragTargetIndicator.target = activeJoint.transform;
			}else if(pullCandidate!=null){
			dragTargetIndicator.target = pullCandidate.transform;
		}else{
			dragTargetIndicator.target = null;
		}
	}

	void startPulling(){

		DistanceJoint2D dst = pullCandidate.GetComponent<DistanceJoint2D>();
		
		dst.connectedBody = this.rigidbody2D;
		activeJoint = dst;
		dst.enabled = true;
		
		strainSFX.Play();

		//pullCandidate.transform.GetComponent<Rock> ().setFixed(false);
	}

	void release(){
		activeJoint.enabled = false;
		activeJoint.connectedBody = null;
		activeJoint = null;

		rigidbody2D.velocity = Vector2.zero;

	}

	void OnTriggerEnter2D(Collider2D o){
		Rock r = o.GetComponent<Rock> ();
		if (r != null) {

			if(pullCandidate!=null){
				Vector2 oldc_pos = pullCandidate.transform.position;
				Vector2 player_pos = transform.position;
				Vector2 newc_pos = o.transform.position;

				float d1 = Vector2.Distance(oldc_pos,player_pos);
				float d2 = Vector2.Distance(newc_pos,player_pos);

				if(d1<d2){
					pullCandidate = o;
				}

			}else{
				pullCandidate = o;
			}

			//swapDragTarget(r);


		}

	}
	/*
	void swapDragTarget(Rock r){
		r.isDragTarget(true);
		if(pullCandidate!=null){
			Rock r_old = pullCandidate.transform.GetComponent<Rock> ();
			Debug.Log("unselect");
			r_old.isDragTarget(false);
		}

	}
	*/

	public void releaseIfDragging(Transform rockTransform){
		DistanceJoint2D rockJoint = rockTransform.GetComponent<DistanceJoint2D>();
		if (rockJoint == activeJoint) {
			release();
		}
	}

	void OnTriggerExit2D(Collider2D o){

		if(pullCandidate == o){
			pullCandidate = null;
		}

	}




}
