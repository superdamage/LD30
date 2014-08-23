using UnityEngine;
using System.Collections;

public class RockGenerator : MonoBehaviour {

	public Transform rockPrefab;
	public int numRocks = 40;

	
	// Use this for initialization
	void Start () {



	}

	public void Generate(Rect worldBounds){

		while (numRocks>0) {

			float halfW = worldBounds.width / 2;
			float halfH = worldBounds.height / 2;
			float rX = Random.Range (-halfW, halfW);
			float rY = Random.Range (-halfH, halfH);

			Vector3 pos = new Vector3 (rX, rY, transform.position.z);
			Transform rock = Instantiate (rockPrefab, pos, Quaternion.identity) as Transform;
			Rock rockGO = rock.gameObject.GetComponent<Rock>();
			rockGO.Dark = Random.Range(0,2)==0;
			rock.parent = this.gameObject.transform;

			numRocks--;

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
