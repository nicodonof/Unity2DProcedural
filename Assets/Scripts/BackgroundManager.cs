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
	void Start () {
		currBkg = Instantiate(background[0]);
		currBkg.transform.localScale = new Vector3(7,5,1);
		mainC = Camera.main;
        mapBeggining = mainC.ScreenToWorldPoint(new Vector3(0, 0, 13));
        currBkg.transform.position = mapBeggining;
		temporaryCackinas = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.realtimeSinceStartup >= 10 * temporaryCackinas){
			var aux = Instantiate(floorDecor[0]);
        	aux.transform.position = mapBeggining + new Vector3(25,2,-1);
			var aux2 = Instantiate(clouds[0]);
        	aux2.transform.position = mapBeggining + new Vector3(30,5,-1);
			
			temporaryCackinas++;
		}
	}
}
