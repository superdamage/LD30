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

	public Renderer nightosphere;

	private bool overViewForced = false;

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

		if (Input.GetKey("m") || overViewForced) {
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

		// set nightosphere
		Color c = nightosphere.material.color;
		c.a = mars.darkness*0.85f;
		nightosphere.material.color = c;

		positionTarget = clampBounds (positionTarget);


		if (speed >= 0) {
			iTween.MoveUpdate (this.gameObject, positionTarget, speed);
			Camera.main.orthographicSize += (cameraSize - Camera.main.orthographicSize)/10;
		}else{
			this.transform.position = positionTarget;
			Camera.main.orthographicSize = cameraSize;
		}

	}

	public void forceOverview(bool forced){
		overViewForced = forced;
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
