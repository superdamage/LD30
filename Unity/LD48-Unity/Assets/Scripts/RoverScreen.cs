using UnityEngine;
using System.Collections;

public class RoverScreen : MonoBehaviour {

	private GUITexture background;
	//private bool powerOn;
	// Use this for initialization
	void Start () {
		background = GetComponent<GUITexture> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setOn(bool _on){
		background.enabled = _on;
	}
	/*
	public void setPowerOn(bool _powerOn){
		//if (_powerOn == powerOn)return;
		_powerOn = powerOn;

		background.enabled = powerOn;
	}
	*/
}
