using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScript : MonoBehaviour {
	Rigidbody2D rigid;
	// BoxCollider2D coll;
	Animator animator;
	public int highscore;
	bool grounded;
	float jumpGas;
	public GameObject levelMaker;
	LevelCreator reference;
	GameObject highscoreText;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		// coll = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
		grounded = true;
		// animator.recorderMode = AnimatorRecorderMode.Playback;
		highscore = 0;
		jumpGas = 4;
		reference = levelMaker.GetComponent<LevelCreator>();
		highscoreText = GameObject.FindGameObjectWithTag("HighscoreText");

	}

	// Update is called once per frame
	void Update () {
		// print(highscore);
		float x = rigid.velocity.x;
		float y = rigid.velocity.y;
		highscore = (int) Mathf.Round(Mathf.Abs(reference.firstBlockReference.transform.position.x));
		highscoreText.GetComponent<TextMeshProUGUI>().SetText(highscore.ToString());
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
		if(Input.GetButton("Jump") ){
			if(Input.GetButtonDown("Jump") && grounded){
				animator.SetBool("Jump", true);
				grounded = false;
				rigid.AddForce(new Vector2(0,300));
			} else {
				if(rigid.velocity.y > 0 && rigid.velocity.y < 10 && jumpGas > 0){
					rigid.AddForce(new Vector2(0, (jumpGas * 5 )));
					jumpGas -= 0.02f;
				}
			}
		}
		// print();


		animator.SetFloat("Speed", rigid.velocity.x);
		animator.SetFloat("SpeedY", rigid.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision){
		// print(collision.otherCollider.gameObject.name + ": " + (collision.otherCollider.bounds.max.y));
		// print(collision.collider.gameObject.name + "- " + (coll.bounds.min.y));
		// print(coll.bounds.min.y + " >= " + collision.otherCollider.bounds.max.y);
		if (collision.gameObject.CompareTag("Floor") && !grounded){ //&& collision.otherCollider.bounds.min.y >= collision.collider.bounds.max.y){
			animator.SetBool("Jump", false);
			if (collision.otherCollider.bounds.min.y >= collision.collider.bounds.max.y) {
				grounded = true;
				rigid.velocity.Set(rigid.velocity.x, 0);
				jumpGas = 4;
			} // print("Groundedhog");
		}else if(collision.gameObject.CompareTag("Enemy")){
			die();
		}
	}

	void OnCollisionExit2D(Collision2D collision){

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.name.Equals("DeathTrigger")) {
			die();
		}
	}

	private void die(){
		SceneManager.LoadScene(Application.loadedLevel);
	}
}
