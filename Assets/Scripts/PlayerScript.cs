using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
  BoxCollider2D coll;
	Animator animator;
	// Use this for initialization
	void Start () {
    rigid = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = rigid.velocity.x;
    float y = rigid.velocity.y;
		if(Input.GetKey("right")){
			rigid.velocity = 
				new Vector2(Mathf.Clamp(x + Time.deltaTime + 1, -7,7),
				y
				);
		} else if(Input.GetKey("left")){
			rigid.velocity = 
				new Vector2(Mathf.Clamp(x - Time.deltaTime - 1, -7,7),
				y
				);
		}
		if(Input.GetButtonDown("Jump") && rigid.velocity.y == 0){
			rigid.AddForce(new Vector2(0,400));
		}

		animator.SetFloat("Speed", rigid.velocity.x);
		animator.SetFloat("SpeedY", rigid.velocity.y);
	}
}
