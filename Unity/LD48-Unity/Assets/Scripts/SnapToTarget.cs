using UnityEngine;
using System.Collections;

public class SnapToTarget : MonoBehaviour {

	public Transform target;

	public Sprite dashTarget;
	public Sprite dotTarget;

	public bool isDash;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (target!=null) {

			isDash = target.GetComponent<Rock>().Dark;


			pos.x = target.position.x;
			pos.y = target.position.y;
			transform.position = pos;

			GetComponent<Renderer>().enabled = true;

			((SpriteRenderer)GetComponent<Renderer>()).sprite = isDash?dashTarget:dotTarget;

		}else{

			GetComponent<Renderer>().enabled = false;
			//enabled = false;
		}
	}
}
