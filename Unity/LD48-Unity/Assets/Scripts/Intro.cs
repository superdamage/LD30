using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

	float dur = 2.0f;
	float introDuration;
	public Transform logo;

	void Start () {
		introDuration = dur;
		logo.GetComponent<Renderer>().enabled = false;
	}
	
	void Update () {
		introDuration -= Time.deltaTime;
		if (introDuration <= (dur-0.1)) {
			logo.GetComponent<Renderer>().enabled = true;
		}

		if (introDuration <= 0) {
			SceneManager.LoadScene ("Title");
		}
	}
}
