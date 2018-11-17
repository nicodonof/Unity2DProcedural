using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public int grid_row = 8;
	public int grid_col = 16;

	public GameObject block;
	// Use this for initialization
	void Start () {
		float widthGrid = Mathf.Round(Camera.main.pixelWidth/grid_row);
		float widthCube = block.GetComponent<BoxCollider2D>().size.x;
		print(widthCube);
		for(int i = 0; i<grid_col;i++){
			GameObject aux = Instantiate(block);
			aux.transform.localScale = new Vector3(1,1,1);
			// aux.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(widthCube / 2  + widthCube /2 * i, widthCube / 2, 11));
			aux.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0,0,11));
			aux.transform.position = new Vector3(
				aux.transform.position.x + (widthCube * i) + (widthCube / 2) ,
				aux.transform.position.y + (widthCube / 2), 
				aux.transform.position.z
				 );


    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
