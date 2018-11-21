using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCreator : MonoBehaviour {
	public int chunkSize = 100;
	float widthCube;
	int chonkIndex;
	Vector3 mapBeggining;
	public GameObject enemy;
	public GameObject block;
	public GameObject left;
	public GameObject right;
	public GameObject floor_one;
	public GameObject plat_middle;
	public GameObject plat_left;
	public GameObject plat_right;
	public GameObject plat_one;
	public Queue<GameObject[]> chunks;
	public GameObject player;
	public GameObject firstBlockReference;
	private PlayerScript ps;
	public bool fake = false;

	public int seed = 0;
	// Use this for initialization
	
	void Start () {
		if (seed != 0) {
			Random.InitState(seed);
		} else {
			seed = Random.Range(0, Int32.MaxValue);
			Random.InitState(seed);
		}
		chonkIndex = 0;
		mapBeggining = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 11));
		widthCube = 1.27f / 2;
		chunks = new Queue<GameObject[]>();
		ps = player.GetComponent<PlayerScript>();
		CreateChunk(true, 0, 0, 0, 0);
		CreateChunk(true, 0, 0, 0, 0);
		CreateChunk(true, 0.05f, 0.05f, 0f, 3);
	}

	// Update is called once per frame
	void Update () {
		if(firstBlockReference.transform.position.x < - ((chonkIndex - chunkSize * 2)  * widthCube) && !fake){
			CreateChunk(false,Mathf.Min(chonkIndex/100f * 0.05f, 0.5f),Mathf.Min(chonkIndex/100f * 0.05f),Mathf.Min(chonkIndex/100f * 0.02f, 0.3f), Mathf.Min(chonkIndex/100, 10));
		} else if(firstBlockReference.transform.position.x < -((chonkIndex - chunkSize * 2) * widthCube)) {
			CreateChunk(true,0,0,0,0);
		}
	}

	void CreateChunk(bool start, float platFreq, float holeFreq, float mobFreq, int size){
		//Dechunk last chunk
		if(!start){
			Dechuncker();
		}

		float rand;
		int randSize;
		float mobRandom;
		GameObject[] chunk = simpleChunk();
		for (int i = 1; i < chunk.Length;) {
			rand = Random.value;
			mobRandom = Random.value;
			randSize = Random.Range(1, size + 1);
			if (rand < holeFreq && i + randSize + 1 < chunk.Length) {
				i = addHole(i, randSize, chunk);
			} else if (rand > holeFreq && rand < holeFreq + platFreq && i + (10-randSize) + 4 < chunk.Length) {
				i = addPlatform(i, 10 - randSize, 3, chunk);
			} else {
				if(mobRandom < mobFreq)
					setEnemy(i, chunk);
				i++;
			}
		}
		chunks.Enqueue(chunk);
		chonkIndex += chunk.Length;
	}

	void setBlock(GameObject aux , int i, GameObject[] chunk, int offsetY = 0){

		aux.transform.SetParent(transform);
		aux.name = "tile_" + i + "_" + chonkIndex;
		aux.transform.position = mapBeggining;
		aux.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chonkIndex * widthCube + firstBlockReference.transform.position.x,
				aux.transform.position.y + widthCube / 2 + offsetY * widthCube,
				aux.transform.position.z
		);

		chunk[i] = aux;
	}

	void setEnemy(int i, GameObject[] chunk){
		GameObject newEnemy = Instantiate(enemy);
		newEnemy.transform.SetParent(transform);
		newEnemy.name = "enemy_" + i + "_" + chonkIndex;
		newEnemy.transform.position = new Vector3(chunk[i].transform.position.x, chunk[i].transform.position.y + 0.5f, chunk[i].transform.position.z);
	}

	GameObject[] simpleChunk(){
		GameObject[] auxChunk = new GameObject[chunkSize];
		bool notInclude = false;
		for (int i = 0; i < chunkSize; i++){
			GameObject aux = Instantiate(block);
			if(firstBlockReference == null){
				firstBlockReference = aux;
				notInclude = true;
			}
			setBlock(aux, i,auxChunk);
		}
		if(notInclude)
			auxChunk[0] = null;
		return auxChunk;
	}


	int addHole(int i, int holeSize, GameObject[] chunk) {
		if (chunk[i - 1] != null) {
			removeChunk(chunk, i-1);
			if (i > 1) {
				setBlock(Instantiate(chunk[i-2] == null ? floor_one: right), i - 1, chunk);
			}
		}
		for (; holeSize > 0; i++, holeSize--) {
			removeChunk(chunk, i);
		}

		removeChunk(chunk, i);
		setBlock(Instantiate(left), i, chunk);
		return i;
	}
	
	int addPlatform(int i, int platSize, int platHeight, GameObject[] chunk) {
		if (chunk[i - 1] != null) {
			removeChunk(chunk, i-1);
			if (i > 1) {
				setBlock(Instantiate(chunk[i-2] == null ? floor_one: right), i - 1, chunk);
			}
		}
		removeChunk(chunk, i++);
		removeChunk(chunk, i);
		setBlock(Instantiate(platSize == 1? plat_one : plat_left), i++, chunk, platHeight);
		platSize--;
		
		for (; platSize > 0; i++) {
			Destroy(chunk[i]);
			if(platSize == 1){
				setBlock(Instantiate(plat_right), i, chunk, platHeight);
			} else {
				setBlock(Instantiate(plat_middle), i, chunk, platHeight);				
			}
			platSize--;
		}
		
		removeChunk(chunk, i++);
		removeChunk(chunk, i);
		setBlock(Instantiate(left), i, chunk);
		return i;
	}

	void removeChunk(GameObject[] chunk, int i) {
		Destroy(chunk[i]);
		chunk[i] = null;
	}

    void Dechuncker(){
        GameObject[] toDechunk = chunks.Dequeue();
        for (int i = 0; i < toDechunk.Length; i++){
			if(toDechunk[i] != null)
            	Destroy(toDechunk[i]);
        }
    }
}

