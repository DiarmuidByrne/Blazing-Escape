using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelCreator : MonoBehaviour {
    public GameObject character;
	public GameObject startTile;
	public GameObject gLeft, gMiddle, gRight, gBlank;
	// Private Variables
	private float startUpPosY;
	private const float TILE_WIDTH = 2.4f;
	private const float RIGHT_TILE_WIDTH = 1.1f;
	private GameObject tmpTile;

	private GameObject collectedTiles;
	private GameObject gameLayer;
	private GameObject player;

	private int blankCounter = 0;
	private int middleCounter = 0;
	private string lastTile = "right";

	private GameObject newEnemy;

	// Time limit for changing trap status
	//private float trapOnTimeLimit = 4f;
	//private float trapOffTimeLimit = 2f;
	//private bool enemyAdded = false;

	// Use this for initialization
	void Start () {
		gameLayer = GameObject.Find ("GameLayer");
		collectedTiles = GameObject.Find ("tiles");
		player = GameObject.Find ("Character");

		for (int i = 0; i<30; i++) {
			GameObject tmpg1 = Instantiate (Resources.Load ("ground_left", typeof(GameObject))) as GameObject;
			tmpg1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			GameObject tmpg2 = Instantiate (Resources.Load ("ground_middle", typeof(GameObject))) as GameObject;
			tmpg2.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			GameObject tmpg3 = Instantiate (Resources.Load ("ground_right", typeof(GameObject))) as GameObject;
			tmpg3.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			GameObject tmpg4 = Instantiate (Resources.Load ("ground_blank", typeof(GameObject))) as GameObject;
			tmpg4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
		}

		/*for (int i = 0; i<10; i++) {
			GameObject tmpg5 = Instantiate (Resources.Load ("laserTrapEnabled", typeof(GameObject))) as GameObject;
			tmpg5.transform.parent = collectedTiles.transform.FindChild ("lTrapOn").transform;
			tmpg5.transform.position = Vector2.zero;
			GameObject tmpg6 = Instantiate (Resources.Load ("laserTrapDisabled", typeof(GameObject))) as GameObject;
			tmpg6.transform.parent = collectedTiles.transform.FindChild ("lTrapOff").transform;
			tmpg6.transform.position = Vector2.zero;
		}*/

		collectedTiles.transform.position = new Vector2(-60.0f, 0f);
		startUpPosY = startTile.transform.position.y;
		FillScene ();

	}

	void FixedUpdate(){
		foreach (Transform child in gameLayer.transform) {
			if (child.position.x < (player.transform.position.x - 20f)) {
				switch (child.gameObject.name) {
				case "ground_left(Clone)":
					child.gameObject.transform.position = gLeft.transform.position;
					child.gameObject.transform.parent = gLeft.transform;
					break;
				case "ground_middle(Clone)":
					child.gameObject.transform.position = gMiddle.transform.position;
					child.gameObject.transform.parent = gMiddle.transform;									
					break;
				case "ground_right(Clone)":
					child.gameObject.transform.position = gRight.transform.position;
					child.gameObject.transform.parent = gRight.transform;
					break;
				case "ground_blank(Clone)":
					child.gameObject.transform.position = gBlank.transform.position;
					child.gameObject.transform.parent = gBlank.transform;
					break;
				/*case "lTrapEnabled(Clone)":
					child.gameObject.transform.position = collectedTiles.transform.FindChild ("lTrapOn").transform.position;
					child.gameObject.transform.parent = collectedTiles.transform.FindChild ("lTrapOn").transform;
					break;*/
				default:
					Destroy (child.gameObject);
					break;
				}
			}
		}

		if (gameLayer.transform.childCount < 25) {
			SpawnTile();

		}
	}

	/*void setTrap() {
		if (trapOnTimeLimit > 1) {
			trapOnTimeLimit -= Time.deltaTime;
		} else {
			if (trapOffTimeLimit > 1){
				
			}
			trapOffTimeLimit -= Time.deltaTime;
			
		}
	}*/

	void SpawnTile() {
		if (blankCounter > 0) {
			SetTile("blank");
			blankCounter--;
			return;
		}
		if (middleCounter > 0) { 
			//randomizeEnemy();
			SetTile("middle");
			middleCounter--;
			return;
		}

		if (lastTile == "blank") {
			SetTile ("left");
			middleCounter = (int)Random.Range (5, 15); 
		} else if (lastTile == "right") {
			blankCounter = (int)Random.Range (5, 7); 
		} else if (lastTile == "middle") {
			SetTile ("right");
			//enemyAdded=false;
		}
	}

	private void FillScene() {

		var rand = Random.Range (15, 25);
		for (int i =0; i < rand; i++) {
			SetTile("middle");

		}
		SetTile ("right");
	}

	/*private void randomizeEnemy() {
		if (enemyAdded) {
			return;
		}

		if((int)Random.Range (0,4) == 1) {
			newEnemy = collectedTiles.transform.FindChild("lTrapOn").transform.GetChild(0).gameObject;
			newEnemy.transform.parent = gameLayer.transform;

			newEnemy.transform.position = new Vector2(startTile.transform.position.x+TILE_WIDTH, startUpPosY + (heightLevel*TILE_WIDTH +(TILE_WIDTH*2)));
			enemyAdded=true;
		}
	}*/

	public void SetTile(string type){
		switch (type) {
		case "left":
			tmpTile = gLeft.transform.GetChild(0).gameObject;
			break;
		case "middle":
			tmpTile = gMiddle.transform.GetChild(0).gameObject;	
			break;
		case "right":
			tmpTile = gRight.transform.GetChild (0).gameObject;	
			break;
		case "blank":
			tmpTile = gBlank.transform.GetChild (0).gameObject;	
			break;
		}

		tmpTile.transform.parent = gameLayer.transform;
        tmpTile.transform.position = new Vector2(startTile.transform.position.x + (TILE_WIDTH), startUpPosY);

		startTile = tmpTile;
		lastTile = type;
	}
}
