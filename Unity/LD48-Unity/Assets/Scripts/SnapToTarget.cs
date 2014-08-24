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
		if (target) {

			isDash = target.GetComponent<Rock>().Dark;


			pos.x = target.position.x;
			pos.y = target.position.y;
			transform.position = pos;

			renderer.enabled = true;

			((SpriteRenderer)renderer).sprite = isDash?dashTarget:dotTarget;
			//enabled = true;
		}else{

			renderer.enabled = false;
			//enabled = false;
		}
	}
}
