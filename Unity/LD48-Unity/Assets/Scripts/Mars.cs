using UnityEngine;
using System.Collections;
using System.Threading;

public class Mars : MonoBehaviour {

	public Sprite[] sprites;
	public Transform tileTemplate;
	public float tileSize = 5;

	public int numTilesX = 4;
	public int numTilesY = 4;

	private int U; //X
	private int V; //Y

	private float spriteSize;

	private float x;
	private float y;
	
	private float genTotal;
	private float gen;

	private Vector2 topLeft = new Vector2();

	public GUIText genTxt;

	void Start () {

		genTxt.enabled = true;

		U = numTilesX; 
		V = numTilesY;

		genTotal = U * V;
		gen = genTotal;
		
		spriteSize = sprites [0].bounds.size.x * tileSize;
		
		Vector2 finalSize = new Vector2 ();
		finalSize.x = spriteSize * (numTilesX-1);
		finalSize.y = spriteSize * (numTilesY-1);
		
		topLeft.x = -(finalSize.x / 2);
		topLeft.y = -(finalSize.y / 2);

		x = topLeft.x;
		y = topLeft.y;
		
		//ThreadStart job = new ThreadStart (GenerateJob);
		//Thread thread = new Thread (job);
		//thread.Start ();

		GenerateJob ();

		genTxt.enabled = false;
	}

	void GenerateJob(){

		while (V>0) {
			
			while (U>0) {
				
				Vector2 pos = new Vector3 (x, y, 0);
				Transform tile = Instantiate (tileTemplate, pos, Quaternion.identity) as Transform;
				Debug.Log(100-(gen/genTotal*100));
				gen--;
				
				
				tile.parent = this.gameObject.transform;
				SpriteRenderer sr = tile.GetComponent ("SpriteRenderer") as SpriteRenderer;
				
				int randomIndex = (int)Mathf.Round(Random.Range (0, sprites.Length));
				sr.sprite = sprites [randomIndex];
				
				tile.localScale = new Vector3 (tileSize, tileSize, 1);
				
				x += spriteSize;
				
				U--;

			}
			
			U = numTilesX;
			x = topLeft.x;
			y += spriteSize;
			V--;

		}



	}
	
	void Update () {
		
	}
}
