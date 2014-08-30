using UnityEngine;
using System.Collections;

public class NextSceneOnSpace : MonoBehaviour {

	public float dismissDuration = 5;
	float timeSceneEntered = 0;
	public string nextScene;

	// Use this for initialization
	void Start () {
		timeSceneEntered = Time.time;
	}
	
	void Update () {
		
		if (Time.time >= (timeSceneEntered + dismissDuration)) {
			dismiss();
		}
		
		if (Input.GetKeyDown ("space") || Input.GetMouseButtonDown(0)) {
			dismiss();
		}
	}
	
	void dismiss(){
		
		Application.LoadLevel (nextScene);
	}
}
