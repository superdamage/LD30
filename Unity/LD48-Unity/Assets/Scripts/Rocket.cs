using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playerJustGitInRocket(){

		SceneManager.LoadScene ("Win");
	}
}
