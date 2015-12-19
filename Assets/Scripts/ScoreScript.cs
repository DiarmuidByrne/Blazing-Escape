using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreScript : MonoBehaviour {
    public Transform character;
    private int bestScore = 0;
    public Text ScoreText;
    public double charX;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        charX = character.transform.position.x * 0.75;
        if(charX > bestScore) {
            updateScore(bestScore);
        }
	}

    // Updates score when incremented and displays to UI
    private void updateScore(int score) {
        score = (int)charX;
        ScoreText.text = score.ToString() + " m";
    }
}
