using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

		SceneManager.LoadScene ("Story1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
