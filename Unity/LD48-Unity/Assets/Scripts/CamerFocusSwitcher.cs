using UnityEngine;
using System.Collections;

public class CamerFocusSwitcher : MonoBehaviour {

	public LevelLoader levelLoader;
	public Transform playerTransform;
	public Transform roverForcusTarget;
	private float defaultCameraSize;

	//public Transform focusTransform;
	public Vector3 positionTarget;

	public bool playerIsInRover = false;


	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;


	public Mars mars;


	// Use this for initialization
	void Start () {

		positionTarget = playerTransform.position;
		defaultCameraSize = Camera.main.orthographicSize;

		//follower.target = focusTransform;
	}
	

	void Update () {

		float speed = 0;
		float cameraSize = defaultCameraSize;

		if (Input.GetKey("m")) {
			positionTarget = levelLoader.transform.position;
			speed = 1.0f;
			cameraSize = levelFocusCameraSize();
		}else if(playerIsInRover){
			positionTarget = roverForcusTarget.position;
			speed = 1.0f;
		}else{
			positionTarget = playerTransform.position;
			speed = 0.2f;
		}

		//roverScreen.transform.enabled = playerIsInRover;
		//roverScreen.setPowerOn(playerIsInRover);

		positionTarget = clampBounds (positionTarget);

		/*
		targetPosition.
		transform.position = Vector3.Lerp(this.transform.position, new Vector3(0.0512155,0.5632893,5.466473), 0.2);
		*/

		/*
		if (smoothness > 0) {

			//focusTransform.Translate(positionTarget-focusTransform.position * smoothness);
			//transform.Translate (transform.position - head.position * smooth);

			//focusTransform.position += (positionTarget-focusTransform.position)/30;

			//focusTransform.position = Vector3.Lerp (focusTransform.position, positionTarget, smoothness);
			focusTransform.position = Vector3.Lerp (positionTarget, focusTransform.position, smoothness);

			// camera size lerp
			Vector2 cn = new Vector2(Camera.main.orthographicSize,Camera.main.orthographicSize);
			Vector2 ct = new Vector2(cameraSize,cameraSize);
			cameraSize = Vector2.Lerp(cn,ct,smoothness).x;

			//Camera.main.orthographicSize += (cameraSize - Camera.main.orthographicSize)/30;

		} else {



		}
		*/

		//iTween.ValueTo(follower
		if (speed >= 0) {
			iTween.MoveUpdate (this.gameObject, positionTarget, speed);
			Camera.main.orthographicSize += (cameraSize - Camera.main.orthographicSize)/10;
		}else{
			this.transform.position = positionTarget;
			Camera.main.orthographicSize = cameraSize;
		}

		//iTween.MoveUpdate (follower.gameObject, positionTarget, speed);
		//focusTransform.position = positionTarget;
		//Camera.main.orthographicSize = cameraSize;
			


	}

	Vector3 clampBounds(Vector3 targetPos){
	
		float vertExtent = Camera.main.camera.orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		
		rightBound = mars.worldBunds.width/2 - horzExtent;
		leftBound = -rightBound;
		
		topBound = mars.worldBunds.height/2 - vertExtent ;
		bottomBound = -topBound;
		
		var pos = new Vector3(targetPos.x, targetPos.y, transform.position.z);
		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
		
		return pos;
	}

	float levelFocusCameraSize(){

		float vertExtent = Camera.main.camera.orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		float p = levelLoader.sentenceSize.x/horzExtent;
		p = Mathf.Max (p,0.9f);

		return defaultCameraSize*p;
	}
}
