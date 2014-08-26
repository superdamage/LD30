using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	float dismissDuration = 5;
	float timeSceneEntered = 0;
	// Use this for initialization
	void Start () {
		timeSceneEntered = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time >= (timeSceneEntered + dismissDuration)) {
			dismiss();
		}
		
		if (Input.GetKeyDown ("space") || Input.GetMouseButtonDown(0)) {
			dismiss();
		}
	}

	void dismiss(){
		
		Application.LoadLevel (0);
	}
}
