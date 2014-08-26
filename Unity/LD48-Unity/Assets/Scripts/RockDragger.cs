using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockDragger : MonoBehaviour {

	private Collider2D pullCandidate;
	public DistanceJoint2D activeJoint = null;

	public AudioSource strainSFX;

	public SnapToTarget dragTargetIndicator;

	private List<Collider2D> pullCandidates;

	// Use this for initialization
	void Start () {
		pullCandidates = new List<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		pullCandidate = closestCandidate();

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

	private Collider2D closestCandidate(){

		float closestDistance = Mathf.Infinity;
		Collider2D closest = null;
		for(int i=0; i<pullCandidates.Count; i++){
			Collider2D rock = pullCandidates[i];
			if(rock == null)continue;

			Vector2 rock_pos = rock.transform.position;
			Vector2 player_pos = transform.position;
			float d = Vector2.Distance(rock_pos,player_pos);
			if(d<closestDistance){
				closest = rock;
				closestDistance = d;
			}
		}

		return closest;
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

			pullCandidates.Add(o);

			/*
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
			*/

			//pullCandidate = o;
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

		pullCandidates.Remove (o);

		/*
		if(pullCandidate == o){
			pullCandidate = null;
		}
		*/

	}




}
