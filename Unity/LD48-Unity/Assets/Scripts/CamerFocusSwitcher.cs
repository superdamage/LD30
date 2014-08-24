using UnityEngine;
using System.Collections;

public class CamerFocusSwitcher : MonoBehaviour {

	private CameraFollowBounds follower;
	public LevelLoader levelLoader;
	public Transform playerTransform;
	private float defaultCameraSize;

	public Transform focusTransform;
	public Vector3 positionTarget;

	private float smoothness = 0;



	// Use this for initialization
	void Start () {
		follower = GetComponent ("CameraFollowBounds") as CameraFollowBounds;
		positionTarget = playerTransform.position;
		defaultCameraSize = Camera.main.orthographicSize;
	}
	

	void Update () {

		float cameraSize = defaultCameraSize;

		if (Input.GetKey("m")) {
			positionTarget = levelLoader.transform.position;
			smoothness = 30.0f;
			cameraSize = levelFocusCameraSize();
		}else{
			positionTarget = playerTransform.position;
			smoothness = 0.0f;

		}

		/*
		targetPosition.

		transform.position = Vector3.Lerp(this.transform.position, new Vector3(0.0512155,0.5632893,5.466473), 0.2);
		*/

		if (smoothness > 0) {
			focusTransform.position = Vector3.Lerp (focusTransform.position, positionTarget, Time.deltaTime * smoothness);

			// camera size lerp
			Vector2 cn = new Vector2(Camera.main.orthographicSize,Camera.main.orthographicSize);
			Vector2 ct = new Vector2(cameraSize,cameraSize);
			cameraSize = Vector2.Lerp(cn,ct,Time.deltaTime * smoothness/6).x;
			Camera.main.orthographicSize = cameraSize;

		} else {
			focusTransform.position = positionTarget;

		}

		Camera.main.orthographicSize = cameraSize;
			

		follower.target = focusTransform;
	}

	float levelFocusCameraSize(){

		float vertExtent = Camera.main.camera.orthographicSize;  
		float horzExtent = vertExtent * Screen.width / Screen.height;
		float p = levelLoader.sentenceSize.x/horzExtent;
		p = Mathf.Max (p,0.9f);

		return defaultCameraSize*p;
	}
}
