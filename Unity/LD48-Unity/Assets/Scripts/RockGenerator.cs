using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockGenerator : MonoBehaviour {

	public Transform rockPrefab;
	public int numRocks = 40;

	private Rect worldBounds;

	public List<Rock> rocks;

	public Transform rockShatterAnimationPrefab;

	
	// Use this for initialization
	void Start () {

		rocks = new List<Rock> ();

	}

	public void Generate(Rect _worldBounds){
		worldBounds = _worldBounds;

		while (numRocks>0) {

			bool dark = Random.Range(0,2)==0;
			addRock(dark);

			numRocks--;

		}
	}

	public Rock addRock(bool dark){

		Vector3 pos = randomPositon();
		Transform rock = Instantiate (rockPrefab, pos, Quaternion.identity) as Transform;
		Rock rockGO = rock.gameObject.GetComponent<Rock>();
		rockGO.Dark = dark;
		rock.parent = this.gameObject.transform;
		rockGO.generator = this;
		rocks.Add(rockGO);

		return rockGO;

	}


	public Vector3 randomPositon(){
		float halfW = worldBounds.width / 2 * 0.5f;
		float halfH = worldBounds.height / 2 * 0.5f;
		float rX = Random.Range (-halfW, halfW);
		float rY = Random.Range (-halfH, halfH);
		
		Vector3 pos = new Vector3 (rX, rY, transform.position.z);
		return pos;
	}

	public void replace(Rock rock){

		Instantiate (rockShatterAnimationPrefab, rock.transform.position, Quaternion.identity);

		Destroy (rock.gameObject);
		numRocks++;
		Generate (worldBounds);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
