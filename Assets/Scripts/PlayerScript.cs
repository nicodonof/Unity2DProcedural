using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    float x = GetComponent<Rigidbody2D>().velocity.x;
    float y = GetComponent<Rigidbody2D>().velocity.y;
		GetComponent<Rigidbody2D>().velocity = 
			new Vector2(Mathf.Clamp(Input.GetAxis("Horizontal") + x, -5,5),y);
		
	}
}
