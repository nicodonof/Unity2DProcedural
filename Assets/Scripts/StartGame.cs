using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	GameObject player;
	// Use this for initialization
	void Start () {
		// player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			Camera.main.GetComponent<LevelMovement>().speed = 0.15f;
			GameObject[] aux = GameObject.FindGameObjectsWithTag("Decor");
			foreach (var item in aux){
				if(item.GetComponent<LevelMovement>() != null){
					item.GetComponent<LevelMovement>().speed = 0.1575f;
				}
			}
		}
		if(Camera.main.transform.position.x < -20.0){
			SceneManager.LoadScene("GameScene");
		}
	}
}
