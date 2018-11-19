using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject[] background;
	public GameObject[] floorDecor;
	public GameObject[] clouds;
	GameObject currBkg;
	Camera mainC;
	// Use this for initialization
	void Start () {
		currBkg = Instantiate(background[0]);
		currBkg.transform.localScale = new Vector3(7,5,1);
		mainC = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mapBeggining = mainC.ScreenToWorldPoint(new Vector3(0, 0, 13));
        currBkg.transform.position = mapBeggining;
		
	}
}
