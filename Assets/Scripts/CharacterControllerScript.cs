using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public bool grounded = false;
	public Transform groundCheck;
	public Light pointLight;
	public Light pointLight2;
	public GameObject player;
	private float move = 0;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 800;
	private float runningTimeLimit = 10f;
	private float stoppedTimeLimit = 4f;
	Animator anim;

    public Text gameOverText;
    public Text ResultText;
    public Canvas gameOverCanvas;
    public Canvas UICanvas;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		pointLight2.range = 8f;
	}

	void Update() {
		//float move = Input.GetAxis ("Horizontal");
		if (grounded && Input.GetKeyDown(KeyCode.Space)) {
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		}
		anim.SetFloat ("speed", Mathf.Abs (move));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (anim.GetFloat("speed") > 0.01) {
            stoppedTimeLimit = 5f;

            if (runningTimeLimit > 1f) {
				// Decrease timeLimit
                if (pointLight2.range < 4.5) {
					pointLight2.range += .1f;
				}
				runningTimeLimit -= Time.deltaTime;
				// If Time limit reaches goal, increase light
				if (pointLight2.range >= 4.5 && pointLight2.range <= 10 && runningTimeLimit < 9f) {
					pointLight2.range +=.08f;
				}
			}
		}
		else {
			if(stoppedTimeLimit > 1f) {
				// Decrease timeLimit.
				runningTimeLimit = 10f;
				if (stoppedTimeLimit > 2f && pointLight.range > 4) {
					pointLight.range -= .09f;
				}
				stoppedTimeLimit -= Time.deltaTime;
				// If Time limit reaches goal, decrease light
				if (stoppedTimeLimit <= 5.2f && pointLight.range >= 0) { 
					pointLight.range -= 0.1f;
					if(pointLight2.range > 4f) {
						pointLight2.range -= 0.1f;
					}
				}
			}
		}

		//pointLight.intensity = 2;
		move = Input.GetAxis ("Horizontal");
		
		// jump
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		//anim.SetBool ("Ground", grounded);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (groundCheck.transform.position.y <= -10f){
            //Application.LoadLevel(0);
            deathMenu();
		}
	}

    public void deathMenu()
    {
        Time.timeScale = 0;
        gameOverCanvas.GetComponent<CanvasGroup>().alpha = 1;
        UICanvas.GetComponent<CanvasGroup>().alpha = 0;

    }
}