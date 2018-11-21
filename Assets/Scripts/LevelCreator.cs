using System.Collections.Generic;
using System.IO;
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
		float rand;
		GameObject[] chunk = simpleChunk();
//		if(chonkIndex < 1){
//			chunk = simpleChunk();
//		} else if (rand > 0.5f) {
//			chunk = floorChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));			
//		} else {
//			chunk = platformChunk(0.9f, 1 + Mathf.RoundToInt(Mathf.Log(ps.highscore, 8) * 2));
//		}
		for (int i = 1; i < chunk.Length;) {
			rand = Random.value;
			if (0.3f < rand && rand < 0.6f && i + 4 < chunk.Length) {
				i = addHole(i, 3, chunk);
			} else if (0.6f < rand && i + 8 < chunk.Length) {
				i = addPlatform(i, 4, 3, chunk);
			} else {
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


	int addHole(int i, int holeSize, GameObject[] chunk) {
		if (chunk[i - 1] != null) {
			Destroy(chunk[i-1]);
			chunk[i - 1] = null;
			if (i > 1) {
				setBlock(Instantiate(chunk[i-2] == null ? floor_one: right), i - 1, chunk);
			}
		}
		for (; holeSize > 0; i++, holeSize--) {
			Destroy(chunk[i]);
			chunk[i] = null;
		}
		Destroy(chunk[i]);
		chunk[i] = null;
		setBlock(Instantiate(left), i, chunk);
		return i;
	}
	
	int addPlatform(int i, int platSize, int platHeight, GameObject[] chunk) {
		if (chunk[i - 1] != null) {
			Destroy(chunk[i-1]);
			chunk[i - 1] = null;
			if (i > 1) {
				setBlock(Instantiate(chunk[i-2] == null ? floor_one: right), i - 1, chunk);
			}
		}
		Destroy(chunk[i]);
		chunk[i++] = null;
		Destroy(chunk[i]);
		chunk[i] = null;
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
		
		Destroy(chunk[i]);
		chunk[i++] = null;
		Destroy(chunk[i]);
		chunk[i] = null;
		setBlock(Instantiate(left), i, chunk);
		return i;
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

