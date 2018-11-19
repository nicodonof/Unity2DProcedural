using UnityEngine;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
	// BoxCollider2D coll;
	Animator animator;
	public int highscore;
	bool grounded;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		// coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		grounded = true;
		// animator.recorderMode = AnimatorRecorderMode.Playback;
		highscore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float x = rigid.velocity.x;
		float y = rigid.velocity.y;
		highscore = (int) ((transform.position.x  + 8.67) * 10);
		bool right = false;
		bool left = Input.GetKey("left");
		if(right || left){
			if(grounded){
				animator.speed = Mathf.Abs(rigid.velocity.x / 8);
				rigid.velocity = new Vector2(
						Mathf.Clamp(x + (Time.deltaTime + 0.7f) * (right?1:-1), -8,8),
						y
				);
			} else {
				// if((right && rigid.velocity.x <= 0) || (left && rigid.velocity.x >= 0)){
					rigid.velocity = new Vector2(
							Mathf.Clamp(x + (Time.deltaTime + 0.2f) * (right?1:-1), -8,8),
							y
					);
				// }
			}
			// print(rigid.velocity.x / 8);
			transform.localScale = new Vector3(right?1:-1, transform.localScale.y, transform.localScale.z);
		} 

		if(Input.GetButtonDown("Jump") && grounded){
			rigid.AddForce(new Vector2(0,700));
			animator.SetBool("Jump", true);
			grounded = false;
			print("Degrounded");
		}
		// print();
		

		animator.SetFloat("Speed", rigid.velocity.x);
		animator.SetFloat("SpeedY", rigid.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision){
		// print(collision.otherCollider.gameObject.name + ": " + (collision.otherCollider.bounds.max.y));
		// print(collision.collider.gameObject.name + "- " + (coll.bounds.min.y));
		// print(coll.bounds.min.y + " >= " + collision.otherCollider.bounds.max.y);
		if (collision.gameObject.tag == "Floor" && !grounded){ //&& collision.otherCollider.bounds.min.y >= collision.collider.bounds.max.y){
				animator.SetBool("Jump", false);
				grounded = true;
				rigid.velocity.Set(rigid.velocity.x,0);
				// print("Groundedhog");
		}
	}

	void OnCollisionExit2D(Collision2D collision){
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("DeathTrigger")) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
