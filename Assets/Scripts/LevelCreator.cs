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
	public GameObject player;
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
		// player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x > (chunkIndex-2) * widthCube * chunkSize){
			CreateChunk(false);
		}
	}

	void CreateChunk(bool start){
		//Dechunk last chunk
		if(!start){
			Dechuncker();
		}

		GameObject[] chunk = floorChunk(0.95f, 10);
		chunks.Enqueue(chunk);

		chunkIndex += 1;
	}

	void Dechuncker(){
		GameObject[] toDechunk = chunks.Dequeue();
		for (int i = 0; i < toDechunk.Length; i++){
			Destroy(toDechunk[i]);
		}
	}

	GameObject[] floorChunk(float holeThreshhold, int holeMaxSize) {
		GameObject[] auxChunk = new GameObject[chunkSize+10];
		for (int i = 0; i < chunkSize; i++){
			GameObject aux = Instantiate(block);
			aux.name = "tile" + i;
			aux.transform.SetParent(gameObject.transform);

			float holeProb = Random.value;
			if(holeProb > holeThreshhold && i < chunkSize + holeMaxSize) {
				i += Random.Range(1, holeMaxSize);  //(int) Mathf.Round((holeProb-0.9f) * 100 + 3);
			}
			aux.transform.position = mapBeggining;
			aux.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chunkSize * chunkIndex * widthCube,
				aux.transform.position.y + widthCube / 2,
				aux.transform.position.z
			);
			auxChunk[i] = aux;
		}

		return auxChunk;
	}

	GameObject[] holedChunk(float holeProb) {
		return null;
	}
}

