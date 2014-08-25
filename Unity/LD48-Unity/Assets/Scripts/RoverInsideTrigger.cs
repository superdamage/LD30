using UnityEngine;
using System.Collections;

public class RoverInsideTrigger : MonoBehaviour {


	public CamerFocusSwitcher focusSwitcher;
	public RoverScreen roverScreen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D o){
		
		if (o.tag == "Player") {
			focusSwitcher.playerIsInRover = true;
			roverScreen.setOn(true);
		}
	}
	
	void OnTriggerExit2D(Collider2D o){
		
		if (o.tag == "Player") {
			focusSwitcher.playerIsInRover = false;
			roverScreen.setOn(false);
		}
	}
}
