using UnityEngine;
using System.Collections;

public class CutsSceneScaleFixer : MonoBehaviour {

	private float defaultCameraScale;
	private float defaultScale;

	// Use this for initialization
	void Start () {
		defaultCameraScale = Camera.main.orthographicSize;
		defaultScale = this.transform.localScale.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		float newScale = defaultScale*Camera.main.orthographicSize/defaultCameraScale;
		this.transform.localScale = new Vector3 (newScale,newScale,newScale);



	}
}
