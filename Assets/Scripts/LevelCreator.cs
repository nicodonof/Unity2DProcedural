using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public int chunkSize = 50;
	float widthCube;
	int chonkIndex;
	Vector3 mapBeggining;
	public GameObject block;
	public Queue<GameObject[]> chunks;
	public GameObject player;

	private PlayerScript ps;
	// Use this for initialization
	void Start () {
		// float widthGrid = Mathf.Round(Camera.main.pixelWidth/grid_row);
		chonkIndex = 0;
		mapBeggining = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 11));
		// block.transform.localScale = new Vector3(0.5f,0.5f,1);
		widthCube = block.GetComponent<BoxCollider2D>().size.x / 2;
		chunks = new Queue<GameObject[]>();
		ps = player.GetComponent<PlayerScript>();
		CreateChunk(true);
		CreateChunk(true);
		CreateChunk(true);
		// player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x > (chonkIndex - 50) * widthCube){
			CreateChunk(false);
		}
	}

	void CreateChunk(bool start){
		//Dechunk last chunk
		if(!start){
			Dechuncker();
		}
		
		//hacer basado en dificultad
		float rand = Random.value;

		GameObject[] chunk;
		if (rand > 0.5f) {
			chunk = floorChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));			
		} else {
			chunk = platformChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));
		}

		chunks.Enqueue(chunk);
		chonkIndex += chunk.Length;
	}

	void Dechuncker() {
		GameObject[] toDechunk = chunks.Dequeue();
		for (int i = 0; i < toDechunk.Length; i++){
			Destroy(toDechunk[i]);
		}
	}

	GameObject[] floorChunk(float holeThreshold, int holeMaxSize) {
		if (holeMaxSize < 1) {
			holeMaxSize = 1;
		}
		GameObject[] auxChunk = new GameObject[chunkSize];
		for (int i = 0; i < chunkSize; i++){
			GameObject aux = Instantiate(block);
			aux.name = "tile" + i;
			aux.transform.SetParent(gameObject.transform);

			float holeProb = Random.value;
			if(holeProb > holeThreshold && i + holeMaxSize < chunkSize) {
				i += Random.Range(1, holeMaxSize);
			}
			aux.transform.position = mapBeggining;
			aux.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chonkIndex * widthCube,
				aux.transform.position.y + widthCube / 2,
				aux.transform.position.z
			);
			auxChunk[i] = aux;
		}

		return auxChunk;
	}

	GameObject[] holedChunk(int size) {
		
		GameObject[] auxChunk = new GameObject[size];
		for (int i = 0; i < size; i += 2) {
			GameObject aux = Instantiate(block);
			aux.name = "tile" + i;
			aux.transform.SetParent(gameObject.transform);

			aux.transform.position = mapBeggining;
			aux.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chonkIndex * widthCube,
				aux.transform.position.y + widthCube / 2,
				aux.transform.position.z
			);
			auxChunk[i] = aux;
		}
		return auxChunk;
	}
	
	GameObject[] platformChunk(float platThreshold, int platMaxSize) {
		int platSize = 0;
		if (platMaxSize < 1) {
			platMaxSize = 1;
		}
		GameObject[] auxChunk = new GameObject[chunkSize];
		for (int i = 0; i < chunkSize; i++) {
			GameObject aux = Instantiate(block);
			aux.name = "tile" + i;
			aux.transform.SetParent(gameObject.transform);
			float holeProb = Random.value;
			if(holeProb > platThreshold && i + platMaxSize< chunkSize) {
				platSize = Random.Range(1, platMaxSize);
				i++;
			}

			aux.transform.position = mapBeggining;
			float y = aux.transform.position.y + widthCube / 2;
			if (platSize > 0) {
				y += widthCube * 8 / 2;
			}

			aux.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chonkIndex * widthCube,
				y,
				aux.transform.position.z
			);

			platSize--;
			auxChunk[i] = aux;
			if (platSize == 1) {
				i+= Random.Range(1, platMaxSize);
			}
		}
		return auxChunk;
	}

}

