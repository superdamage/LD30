using UnityEngine;
using System.Collections;

public class VisibleOnTrigger : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D o){
		
		if (o.tag == "Player" && o.isTrigger==false) {
			Debug.Log("pl");
			//focusSwitcher.playerIsInRover = true;
			spriteRenderer.enabled = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D o){
		
		if (o.tag == "Player" && o.isTrigger==false) {
			Debug.Log("pl-e");
			//focusSwitcher.playerIsInRover = false;
			spriteRenderer.enabled = false;
		}
	}
}
