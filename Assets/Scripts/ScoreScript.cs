using UnityEngine;
using System.Collections;

public class ScoreScript : MonoBehaviour {
	public Transform camera;
	public int camX;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		camX = (int)(camera.transform.position.x +6.01f);
	}

	void OnGUI() {
		GUI.Label(new Rect (600,5,50,50), camX.ToString()+ " m");
	}
}
