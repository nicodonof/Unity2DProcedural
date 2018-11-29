using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject[] background;
	public GameObject[] floorDecor;
	public GameObject[] clouds;
	GameObject currBkg;
    Vector3 mapBeggining;
	Camera mainC;	// Use this for initialization
	// private GameObject[] currFloor;
	private GameObject[] currFloor;
	private GameObject[] currClouds;

	private PlayerScript playerScript;

	private int levelIndex = 0;

	void Start () {
		currBkg = Instantiate(background[levelIndex]);
		currBkg.transform.SetParent(transform);
		currBkg.transform.localScale = new Vector3(7,5,1);
		mainC = Camera.main;
        mapBeggining = mainC.ScreenToWorldPoint(new Vector3(0, 0, 13));
        currBkg.transform.position = mapBeggining;
		currFloor = new GameObject[2];
		currClouds = new GameObject[2];
		currFloor[0] = Instantiate(floorDecor[levelIndex]);
		currFloor[0].transform.SetParent(transform);
		currFloor[1] = Instantiate(floorDecor[levelIndex]);
		currFloor[1].transform.SetParent(transform);
		currClouds[0] = Instantiate(clouds[levelIndex]);
		currClouds[0].transform.SetParent(transform);
		currClouds[1] = Instantiate(clouds[levelIndex]);
		currClouds[1].transform.SetParent(transform);
		playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

		startParalax();
	}

	private void setNewBackground(){
		currBkg.GetComponent<SpriteRenderer>().sprite = background[levelIndex].GetComponent<SpriteRenderer>().sprite;
		currFloor[0].GetComponent<SpriteRenderer>().sprite = floorDecor[levelIndex].GetComponent<SpriteRenderer>().sprite;
		currFloor[1].GetComponent<SpriteRenderer>().sprite = floorDecor[levelIndex].GetComponent<SpriteRenderer>().sprite;
		currClouds[0].GetComponent<SpriteRenderer>().sprite = clouds[levelIndex].GetComponent<SpriteRenderer>().sprite;
		currClouds[1].GetComponent<SpriteRenderer>().sprite = clouds[levelIndex].GetComponent<SpriteRenderer>().sprite;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(playerScript.highscore >= 100 && levelIndex == 0){
			levelIndex = 1;
			setNewBackground();
		} else if(playerScript.highscore >= 200 && levelIndex == 1){
			levelIndex = 2;
			setNewBackground();
		} else if(playerScript.highscore >= 300 && levelIndex == 2){
			levelIndex = 3;
			setNewBackground();
		}
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
