using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public int chunkSize = 50;
	float widthCube;
	int chunkIndex;
	Vector3 mapBeggining;
	public GameObject block;
	public Queue<GameObject[]> chunks;
	GameObject player;
	// Use this for initialization
	void Start () {
		// float widthGrid = Mathf.Round(Camera.main.pixelWidth/grid_row);
		chunkIndex = 0;
		mapBeggining = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 11));
		// block.transform.localScale = new Vector3(0.5f,0.5f,1);
		widthCube = block.GetComponent<BoxCollider2D>().size.x / 2;
		chunks = new Queue<GameObject[]>();
		CreateChunk(true);
		CreateChunk(true);
		CreateChunk(true);
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x > ((chunkIndex-2) * widthCube * chunkSize)){
			CreateChunk(false);
		}
	}

	void CreateChunk(bool start){
		//Dechunk last chunk
		if(!start){
			Dechucker();
		}

		//Create the floor
    	GameObject[] auxChunk = new GameObject[chunkSize];
		for (int i = 0; i < chunkSize; i++){
			GameObject aux = Instantiate(block);
			// aux.transform.localScale = new Vector3(1, 1, 1);
			// aux.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(widthCube / 2  + widthCube /2 * i, widthCube / 2, 11));
			float pozo = Random.value;
			if(pozo > 0.95 && i < chunkSize + 6){
				i += (int) Mathf.Round(((pozo-0.9f) * 100) + 3);
			}
			aux.transform.position = mapBeggining;
			aux.transform.position = new Vector3(
			aux.transform.position.x + (widthCube / 2) + (widthCube * i) + (chunkSize * chunkIndex * widthCube),
			aux.transform.position.y + (widthCube / 2),
			aux.transform.position.z
			);
			auxChunk[i] = aux;
  		}	
		chunks.Enqueue(auxChunk);

		chunkIndex += 1;
	}

	void Dechucker(){
		GameObject[] toDechunk = chunks.Dequeue();
		for (int i = 0; i < toDechunk.Length; i++){
			Destroy(toDechunk[i]);
		}
	}
}


public class Chunk {

}