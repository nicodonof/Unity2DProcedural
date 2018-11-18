using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	private GameObject player;
	private float offset;
	
	public float y;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		offset = gameObject.transform.position.x - player.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			player.transform.position.x + offset,
			y,
			-10
		);
	}
}
