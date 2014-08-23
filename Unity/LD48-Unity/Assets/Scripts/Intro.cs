using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	float introDuration = 2.5f;

	void Start () {
		
	}
	
	void Update () {
		introDuration -= Time.deltaTime;
		if (introDuration <= 0) {
			Application.LoadLevel(@"Title");
		}
	}
}
