using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;
	public bool grounded = false;
	public Transform groundCheck;
	public Light pointLight;
	public Light pointLight2;
	public GameObject player;
	private float move = 0;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700;
	private float runningTimeLimit = 10f;
	private float stoppedTimeLimit = 4f;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		pointLight.range = 6f;
		pointLight2.range = 4f;
	}

	void Update() {
		//float move = Input.GetAxis ("Horizontal");
		if (grounded && Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
		anim.SetFloat ("speed", Mathf.Abs (move));
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (anim.GetFloat("speed") > 0.01) {
			if(runningTimeLimit > 1f) {
				// Decrease timeLimit.
				stoppedTimeLimit = 8f;
				if(move < 1.5) {

				}
				if (runningTimeLimit > 7f && pointLight.range < 4.5) {
					pointLight.range += .1f;
				}
				runningTimeLimit -= Time.deltaTime;
//				Debug.Log(runningTimeLimit);
				// If Time limit reaches goal, increase light
				if (runningTimeLimit <= 7f && pointLight.range <= 9) {
					pointLight.range +=.08f;
					pointLight2.range +=.08f;
				}
			}
		}
		else if (anim.GetFloat("speed") <= 0.01) {
			if(stoppedTimeLimit > 1f) {
				// Decrease timeLimit.
				runningTimeLimit = 10f;
				if (stoppedTimeLimit > 2f && pointLight.range > 4) {
					pointLight.range -= .5f;
				}
				stoppedTimeLimit -= Time.deltaTime;
				//Debug.Log(stoppedTimeLimit);
				// If Time limit reaches goal, decrease light
				if (stoppedTimeLimit <= 5.2f && pointLight.range >= 0) { 
					pointLight.range -= 0.1f;
					if(pointLight2.range > 4f) {
						pointLight2.range -= 0.1f;
					}
				}
			}
		}

		pointLight.intensity = (int)2;
		move = Input.GetAxis ("Horizontal");
		// jump
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//anim.SetBool ("Ground", grounded);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (groundCheck.transform.position.y <= -10f){
			Application.LoadLevel(0);
		}
	}
}
