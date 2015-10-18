using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		float x = target.transform.position.x;

		transform.position = new Vector3 ((x+2f), (0.48f), -10f);
	
	}
}
