using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
  BoxCollider2D coll;
	Animator animator;
	bool grounded;
	// Use this for initialization
	void Start () {
    rigid = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		grounded = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = rigid.velocity.x;
    float y = rigid.velocity.y;
		if(Input.GetKey("right")){
			rigid.velocity = new Vector2(
					Mathf.Clamp(x + Time.deltaTime + 1, -7,7),
					y
			);
		} else if(Input.GetKey("left")){
			rigid.velocity = new Vector2(
				Mathf.Clamp(x - Time.deltaTime - 1, -7,7),
				y
			);
		}
		if(Input.GetButtonDown("Jump") && grounded){
			rigid.AddForce(new Vector2(0,700));
			animator.SetBool("Jump", true);
			grounded = false;
		}
		// print();
		

		animator.SetFloat("Speed", rigid.velocity.x);
		animator.SetFloat("SpeedY", rigid.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// RaycastHit2D hit = Physics2D.Raycast(new Vector2(coll.bounds.min.x, coll.bounds.min.y), -Vector2.up, 0.01f);
		if (coll.gameObject.CompareTag("Floor")) {
			animator.SetBool("Jump", false);
			grounded = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("DeathTrigger")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
