using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	float dur = 2.0f;
	float introDuration;
	public Transform logo;

	void Start () {
		introDuration = dur;
		logo.renderer.enabled = false;
	}
	
	void Update () {
		introDuration -= Time.deltaTime;
		if (introDuration <= (dur-0.1)) {
			logo.renderer.enabled = true;
		}

		if (introDuration <= 0) {
			Application.LoadLevel(@"Title");
		}
	}
}
