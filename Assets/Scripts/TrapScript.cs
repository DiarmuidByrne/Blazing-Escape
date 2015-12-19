using UnityEngine;
using System.Collections;

public class TrapScript : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			
	}

	void OnTriggerEnter2D(Collider2D coll) {
		Debug.Log ("Object Entered Collider");
		if (coll.gameObject.tag == "Player") {
			GameObject tmpPlayer = GameObject.Find ("Character");
			tmpPlayer.GetComponent<Collider2D>().enabled = false;
			tmpPlayer.GetComponent<Collider2D>().enabled = false;
			BoxCollider2D boxCol = tmpPlayer.GetComponent<BoxCollider2D>();
			tmpPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 650));
			boxCol.enabled = false;
		}
	}
}
