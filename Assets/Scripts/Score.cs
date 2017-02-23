using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    private float score = 0.0f;
    private int difficultyLevel = 1;
    private int maxDifficultylevel = 10;
    private int scoreToNext = 10;
    public Text scoreText;
    private bool isDead =false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (isDead) return;
        if (score >= scoreToNext) { LevelUp(); }
        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
	}

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultylevel) return;
        scoreToNext *= 2;
        difficultyLevel++;
        GetComponent<Runner>().SetSpeed(difficultyLevel);
    }
    public void OnDeath()
    {
        isDead = true;
    }
}
