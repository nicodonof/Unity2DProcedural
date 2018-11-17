using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {
	public int grid_row = 8;
	public int grid_col = 16;

	public GameObject block;
	// Use this for initialization
	void Start () {
		for(int i = 0; i<grid_col;i++){
			// Instantiate(block);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
