using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public int chunkSize = 50;
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
	// Use this for initialization
	void Start () {
//		left = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Levels/left.prefab");
		// float widthGrid = Mathf.Round(Camera.main.pixelWidth/grid_row);
		chonkIndex = 0;
		mapBeggining = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 11));
		// block.transform.localScale = new Vector3(0.5f,0.5f,1);
		// widthCube = block.GetComponent<BoxCollider2D>().size.x / 2; //
		widthCube = 1.27f / 2;
		chunks = new Queue<GameObject[]>();
		ps = player.GetComponent<PlayerScript>();
		CreateChunk(true);
		CreateChunk(true);
		CreateChunk(true);
		// player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if(firstBlockReference.transform.position.x < - ((chonkIndex - chunkSize * 2)  * widthCube) && !fake){
			CreateChunk(false);
		} else if(firstBlockReference.transform.position.x < -((chonkIndex - chunkSize * 2) * widthCube)) {
			CreateChunk(true);
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
		if(chonkIndex < 1 || start){
			chunk = simpleChunk();
		} else if (rand > 0.5f) {
			chunk = floorChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));
		} else {
			chunk = platformChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));
		}

		chunks.Enqueue(chunk);
		chonkIndex += chunk.Length;
	}

	GameObject[] floorChunk(float holeThreshold, int holeMaxSize) {
		if (holeMaxSize < 1) {
			holeMaxSize = 1;
		}
		GameObject[] auxChunk = new GameObject[chunkSize];
		for (int i = 0; i < chunkSize; i++){


			float holeProb = Random.value;
			if(holeProb > holeThreshold && i + holeMaxSize + 2 < chunkSize && i > 1) {
				if (auxChunk[i - 2] == null) {
					auxChunk[i - 1].GetComponent<SpriteRenderer>().sprite =
						floor_one.GetComponent<SpriteRenderer>().sprite;
				} else {
					GameObject aux = Instantiate(right);
					setBlock(Instantiate(right), i, auxChunk);
				}
				int holeSize = Random.Range(2, holeMaxSize);
				setBlock(Instantiate(left), i + holeSize,auxChunk);
				i += holeSize;
			} else {
				GameObject aux = Instantiate(block);
				setBlock(aux,i,auxChunk);
			}
		}

		return auxChunk;
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

	void setEnemy(GameObject aux , int i, GameObject[] chunk, int offsetY = 0){
		GameObject newEnemy = Instantiate(enemy);
		enemy.transform.position = new Vector3(
				aux.transform.position.x + widthCube / 2 + widthCube * i + chonkIndex * widthCube + firstBlockReference.transform.position.x,
				aux.transform.position.y + widthCube / 2 + offsetY * widthCube + 0.5f,
				aux.transform.position.z
		);
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

	GameObject[] platformChunk(float platThreshold, int platMaxSize) {
		int platSize = 0;
		if (platMaxSize < 1) {
			platMaxSize = 1;
		}
		GameObject[] auxChunk = new GameObject[chunkSize];
		bool puttingPlats = false;
		int platHeight = 0;
		for (int i = 0; i < chunkSize; i++) {
			float holeProb = Random.value;
			if(!puttingPlats && holeProb > platThreshold && i + platMaxSize + 4 /* para q meta bien los bordes del piso */ < chunkSize && i > 0) {
				platSize = Random.Range(1, platMaxSize);
				auxChunk[i - 1].GetComponent<SpriteRenderer>().sprite = right.GetComponent<SpriteRenderer>().sprite;
				puttingPlats = true;
				platHeight = Random.Range(4, 8);
				i++;
				setBlock(Instantiate(platSize == 1? plat_one : plat_left), i++, auxChunk, platHeight);
				platSize--;
				if (platSize == 0) {
					puttingPlats = false;
					i++;
					setBlock(Instantiate(left), i++, auxChunk);
				}
			}

			if(puttingPlats){
				GameObject aux = Instantiate(plat_middle);
				setBlock(aux, i, auxChunk, platHeight);
				if(platSize == 1){
					setBlock(Instantiate(plat_right), ++i, auxChunk, platHeight);
					puttingPlats = false;
					i++;
					setBlock(Instantiate(left), ++i, auxChunk);
				}

				platSize--;
			} else {
                GameObject aux = Instantiate(block);
				setBlock(aux, i, auxChunk);
            }

		}
		return auxChunk;
	}


    void Dechuncker(){
        GameObject[] toDechunk = chunks.Dequeue();
        for (int i = 0; i < toDechunk.Length; i++){
			if(toDechunk[i] != null)
            	Destroy(toDechunk[i]);
        }
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

}

