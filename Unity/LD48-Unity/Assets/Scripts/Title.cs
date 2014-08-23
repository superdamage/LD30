using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {

	public GUITextButton sdButton;
	public GUITextButton playButton;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
	
		sdButton.OnButtonMouseUp += onOpenSDSite;
		playButton.OnButtonMouseUp += onPlay;
	}

	void onOpenSDSite(){
		Application.OpenURL (@"http://superdamage.com");
	}

	void onPlay(){
		Debug.Log (@"SEPS");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
