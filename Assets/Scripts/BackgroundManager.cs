using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject[] background;
	public GameObject[] floorDecor;
	public GameObject[] clouds;
	GameObject currBkg;
    Vector3 mapBeggining;
	Camera mainC;
	int temporaryCackinas;
	// Use this for initialization
	// private GameObject[] currFloor;
	private GameObject[] currFloor;
	private GameObject[] currClouds;

	void Start () {
		currBkg = Instantiate(background[0]);
		currBkg.transform.SetParent(transform);
		currBkg.transform.localScale = new Vector3(7,5,1);
		mainC = Camera.main;
        mapBeggining = mainC.ScreenToWorldPoint(new Vector3(0, 0, 13));
        currBkg.transform.position = mapBeggining;
		temporaryCackinas = 0;
		currFloor = new GameObject[2];
		currClouds = new GameObject[2];
		currFloor[0] = Instantiate(floorDecor[0]); 
		currFloor[0].transform.SetParent(transform);
		currFloor[1] = Instantiate(floorDecor[0]);
		currFloor[1].transform.SetParent(transform);
		currClouds[0] = Instantiate(clouds[0]);
		currClouds[0].transform.SetParent(transform);
		currClouds[1] = Instantiate(clouds[0]);
		currClouds[1].transform.SetParent(transform);

		startParalax();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(currFloor[0].transform.position.x < -15){
            currFloor[0].transform.position = mapBeggining + new Vector3(Random.Range(20f, 30f), 2, -1);
        }
		if(currFloor[1].transform.position.x < -15){
            currFloor[1].transform.position = mapBeggining + new Vector3(Random.Range(20f, 30f), 2, -1);
        }
		if(currClouds[0].transform.position.x < -15){
            currClouds[0].transform.position = mapBeggining + new Vector3(Random.Range(20f, 30f), Random.Range(2f, 10f), -1);
        }
		if(currClouds[1].transform.position.x < -15){
            currClouds[1].transform.position = mapBeggining + new Vector3(Random.Range(20f, 30f), Random.Range(2f, 10f), -1);
        }
	}

	void startParalax(){
        currClouds[0].transform.position = mapBeggining + new Vector3(Random.Range(15f, 20f), Random.Range(2f, 10f), -1);
        currClouds[1].transform.position = mapBeggining + new Vector3(Random.Range(25f, 35f), Random.Range(2f, 10f), -1);
        currFloor[0].transform.position = mapBeggining + new Vector3(Random.Range(20f, 23f), 2, -1);
        currFloor[1].transform.position = mapBeggining + new Vector3(Random.Range(27f, 30f), 2, -1);
	}
}
