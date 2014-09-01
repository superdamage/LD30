using UnityEngine;
using System.Collections;

public class RoverInsideTrigger : MonoBehaviour {


	public CamerFocusSwitcher focusSwitcher;
	public RoverScreen roverScreen;
	public AudioSource audioSource;
	public float setOnPitch = 1.0f;
	public float setOffPitch = 1.0f;
	public Mars mars;


	// Use this for initialization
	void Start () {
		//audioSource = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D o){
		
		if (o.tag == "Player" && focusSwitcher.playerIsInRover == false) {

			audioSource.Stop();
			audioSource.pitch = setOnPitch;
			audioSource.Play();


			focusSwitcher.playerIsInRover = true;
			roverScreen.setOn(true);

			mars.timeFrozen = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D o){

			if (o.tag == "Player" && focusSwitcher.playerIsInRover == true) {



			focusSwitcher.playerIsInRover = false;
			roverScreen.setOn(false);

			audioSource.Stop();
			audioSource.pitch = setOffPitch;
			audioSource.Play();

			mars.timeFrozen = false;

		}
	}
}
