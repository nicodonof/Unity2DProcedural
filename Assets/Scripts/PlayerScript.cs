using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
	BoxCollider2D coll;
	Animator animator;
	public int highscore;
	bool grounded;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		grounded = false;
		// animator.recorderMode = AnimatorRecorderMode.Playback;
		highscore = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = rigid.velocity.x;
		float y = rigid.velocity.y;
		highscore = (int) ((transform.position.x  + 8.67) * 10);
		if(Input.GetKey("right")){
			rigid.velocity = new Vector2(
					Mathf.Clamp(x + Time.deltaTime + 0.5f, -8,8),
					y
			);
			// print(rigid.velocity.x / 8);
			animator.speed = rigid.velocity.x / 8;
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		} else if(Input.GetKey("left")){
			rigid.velocity = new Vector2(
				Mathf.Clamp(x - Time.deltaTime - 0.5f, -8,8),
				y
			);
			animator.speed = rigid.velocity.x / 8;
			// print(rigid.velocity.x / 8);
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
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

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Floor" && collision.otherCollider.bounds.min.y <= coll.bounds.max.y){
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
